using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TodoList.Api.Data;
using TodoList.Api.DTOs.Checklist;
using TodoList.Api.Models;

namespace TodoList.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ChecklistController : ControllerBase
{
    private readonly TodoDbContext _context;

    public ChecklistController(TodoDbContext context)
    {
        _context = context;
    }

    private int GetUserId() => int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ChecklistResponse>>> GetChecklists()
    {
        var userId = GetUserId();
        var checklists = await _context.Checklists
            .Include(c => c.Items)
            .Where(c => c.UserId == userId)
            .Select(c => new ChecklistResponse(
                c.Id,
                c.Title,
                c.Items.Select(i => new TodoItemResponse(i.Id, i.Description, i.IsCompleted))
            ))
            .ToListAsync();

        return checklists;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ChecklistResponse>> GetChecklist(int id)
    {
        var userId = GetUserId();
        var checklist = await _context.Checklists
            .Include(c => c.Items)
            .FirstOrDefaultAsync(c => c.Id == id && c.UserId == userId);

        if (checklist == null)
            return NotFound();

        return new ChecklistResponse(
            checklist.Id,
            checklist.Title,
            checklist.Items.Select(i => new TodoItemResponse(i.Id, i.Description, i.IsCompleted))
        );
    }

    [HttpPost]
    public async Task<ActionResult<ChecklistResponse>> CreateChecklist(CreateChecklistRequest request)
    {
        var checklist = new Checklist
        {
            Title = request.Title,
            UserId = GetUserId()
        };

        _context.Checklists.Add(checklist);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetChecklist), new { id = checklist.Id },
            new ChecklistResponse(checklist.Id, checklist.Title, Array.Empty<TodoItemResponse>()));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteChecklist(int id)
    {
        var userId = GetUserId();
        var checklist = await _context.Checklists
            .FirstOrDefaultAsync(c => c.Id == id && c.UserId == userId);

        if (checklist == null)
            return NotFound();

        _context.Checklists.Remove(checklist);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpPost("{id}/items")]
    public async Task<ActionResult<TodoItemResponse>> CreateTodoItem(int id, CreateTodoItemRequest request)
    {
        var userId = GetUserId();
        var checklist = await _context.Checklists
            .FirstOrDefaultAsync(c => c.Id == id && c.UserId == userId);

        if (checklist == null)
            return NotFound();

        var todoItem = new TodoItem
        {
            Description = request.Description,
            ChecklistId = id
        };

        _context.TodoItems.Add(todoItem);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetTodoItem), new { id = checklist.Id, itemId = todoItem.Id },
            new TodoItemResponse(todoItem.Id, todoItem.Description, todoItem.IsCompleted));
    }

    [HttpGet("{id}/items/{itemId}")]
    public async Task<ActionResult<TodoItemResponse>> GetTodoItem(int id, int itemId)
    {
        var userId = GetUserId();
        var todoItem = await _context.TodoItems
            .Include(t => t.Checklist)
            .FirstOrDefaultAsync(t => t.Id == itemId && t.ChecklistId == id && t.Checklist.UserId == userId);

        if (todoItem == null)
            return NotFound();

        return new TodoItemResponse(todoItem.Id, todoItem.Description, todoItem.IsCompleted);
    }

    [HttpPut("{id}/items/{itemId}")]
    public async Task<IActionResult> UpdateTodoItem(int id, int itemId, UpdateTodoItemRequest request)
    {
        var userId = GetUserId();
        var todoItem = await _context.TodoItems
            .Include(t => t.Checklist)
            .FirstOrDefaultAsync(t => t.Id == itemId && t.ChecklistId == id && t.Checklist.UserId == userId);

        if (todoItem == null)
            return NotFound();

        todoItem.Description = request.Description;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpPatch("{id}/items/{itemId}/status")]
    public async Task<IActionResult> UpdateTodoItemStatus(int id, int itemId, UpdateTodoItemStatusRequest request)
    {
        var userId = GetUserId();
        var todoItem = await _context.TodoItems
            .Include(t => t.Checklist)
            .FirstOrDefaultAsync(t => t.Id == itemId && t.ChecklistId == id && t.Checklist.UserId == userId);

        if (todoItem == null)
            return NotFound();

        todoItem.IsCompleted = request.IsCompleted;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}/items/{itemId}")]
    public async Task<IActionResult> DeleteTodoItem(int id, int itemId)
    {
        var userId = GetUserId();
        var todoItem = await _context.TodoItems
            .Include(t => t.Checklist)
            .FirstOrDefaultAsync(t => t.Id == itemId && t.ChecklistId == id && t.Checklist.UserId == userId);

        if (todoItem == null)
            return NotFound();

        _context.TodoItems.Remove(todoItem);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
