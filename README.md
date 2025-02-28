# ITA Todo List API (.NET)

API Todo List sederhana yang dibangun menggunakan .NET 8.0, Entity Framework Core, dan PostgreSQL.

## Teknologi yang Digunakan

- .NET 8.0
- Entity Framework Core
- PostgreSQL
- JWT Authentication
- REST API

## Persyaratan Sistem

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [PostgreSQL](https://www.postgresql.org/download/)
- [Visual Studio Code](https://code.visualstudio.com/) atau [JetBrains Rider](https://www.jetbrains.com/rider/)

## Quick Start

### 1. Persiapan Database

1. Buat database PostgreSQL baru dengan nama `ita_todo_list`:
   ```sql
   CREATE DATABASE ita_todo_list;
   ```

2. Sesuaikan connection string di `appsettings.Development.json`:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Host=localhost;Database=ita_todo_list;Username=postgres;Password=postgres"
     }
   }
   ```

### 2. Menjalankan Aplikasi

1. Clone repository:
   ```bash
   git clone https://github.com/your-username/ita-todo-list-api-dotnet.git
   cd ita-todo-list-api-dotnet
   ```

2. Restore dependencies:
   ```bash
   dotnet restore
   ```

3. Jalankan migrasi database:
   ```bash
   cd TodoList.Api
   dotnet ef database update --project TodoList.Api/
   ```

4. Jalankan aplikasi:
   ```bash
   dotnet watch --project TodoList.Api/
   ```

API akan berjalan di:
- HTTP: http://localhost:5000
- HTTPS: https://localhost:7000

### 3. Pengujian API

#### Menggunakan Visual Studio Code
Anda dapat menguji API menggunakan file `TodoList.Api.http`. Ikuti langkah berikut:

1. Buka file `TodoList.Api.http` di Visual Studio Code
2. Install ekstensi "REST Client"
3. Pilih environment dengan klik "No Environment" di pojok kanan bawah
4. Pilih environment "dev" untuk HTTP atau "https" untuk HTTPS
5. Jalankan request dalam urutan berikut:
   - Register: Daftar user baru
   - Login: Dapatkan token JWT
   - Buat Checklist: Buat checklist baru
   - Buat Todo Item: Tambahkan item ke checklist

#### Menggunakan JetBrains Rider
JetBrains Rider memiliki fitur HTTP Client bawaan. Ikuti langkah berikut:

1. Buka file `TodoList.Api.http`
2. Klik icon "Run All Requests" di pojok kiri atas file untuk menjalankan semua request, atau
3. Klik icon "Run Request" (▶️) di sebelah setiap request untuk menjalankan request tertentu
4. Untuk mengatur environment:
   - Klik dropdown "No Environment" di toolbar HTTP Client
   - Pilih "dev" untuk HTTP atau "https" untuk HTTPS
5. Fitur tambahan di Rider:
   - Response akan ditampilkan di tab "Response" di bagian bawah
   - History request tersimpan di tab "HTTP Client" di bagian kiri
   - Variabel environment dapat diedit di "Tools > HTTP Client > Environment Variables"

## Endpoint API

### Autentikasi

- `POST /api/auth/register` - Registrasi user baru
- `POST /api/auth/login` - Login dan dapatkan token JWT

### Checklist

- `GET /api/checklist` - Ambil semua checklist
- `GET /api/checklist/{id}` - Ambil checklist spesifik
- `POST /api/checklist` - Buat checklist baru
- `DELETE /api/checklist/{id}` - Hapus checklist

### Todo Item

- `GET /api/checklist/{id}/items/{itemId}` - Ambil item spesifik
- `POST /api/checklist/{id}/items` - Buat item baru
- `PUT /api/checklist/{id}/items/{itemId}` - Update item
- `PATCH /api/checklist/{id}/items/{itemId}/status` - Update status item
- `DELETE /api/checklist/{id}/items/{itemId}` - Hapus item

## Keamanan

- Semua endpoint kecuali register dan login memerlukan JWT token
- Token harus disertakan di header: `Authorization: Bearer {token}`
- Token berlaku selama 60 menit

## Troubleshooting

1. **Error Connection String**
   - Pastikan PostgreSQL sudah berjalan
   - Verifikasi username dan password di connection string
   - Pastikan database `ita_todo_list` sudah dibuat

2. **Error Migrasi**
   - Hapus folder `Migrations` jika ada
   - Jalankan `dotnet ef migrations add InitialCreate --project TodoList.Api/`
   - Jalankan `dotnet ef database update --project TodoList.Api/`

3. **Error HTTPS**
   - Untuk development, gunakan HTTP (port 5000)
   - Atau generate dev certificate: `dotnet dev-certs https --trust`
