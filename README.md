# WebApplicationNawatech

Sebuah aplikasi web sederhana berbasis ASP.NET MVC untuk keperluan coding test Nawatech Junior Developer.

## 🚀 Fitur

- Register & Login User
- Edit Profil + Upload Foto
- Manajemen Kategori Produk (Create, Edit, Soft Delete)
- Manajemen Produk (Create, Edit, Soft Delete)
- Autentikasi menggunakan Session
- Upload gambar produk dan user profile

## 🛠️ Teknologi

- ASP.NET MVC 
- Entity Framework Core
- SQL Server
- Bootstrap
- Razor View

## ⚙️ Cara Menjalankan Project

1. **Clone repository**
   ```bash
   git clone https://github.com/annisauliawahyudi/WebApplicationNawatech.git

2. **Buka project dengan Visual Studio**

- File > Open > Project/Solution
- Pilih WebApplicationNawatech.sln

**3. Atur koneksi database**
- Buka appsettings.json
- Ubah bagian "DefaultConnection" ke string koneksi SQL Server lokal kamu.
  
  "ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=NawatechDb;Trusted_Connection=True;TrustServerCertificate=True;"
}

**4. Jalankan Migrasi (jika belum ada database)**
- Buka Package Manager Console di Visual Studio: Update-Database

5. Jalankan aplikasi
- Tekan F5 atau klik Start Debugging
- plikasi akan terbuka di browser (biasanya di https://localhost:xxxx/)

**6. Login atau Registrasi**
- Daftar user baru melalui halaman register.
- Atau login dengan:
    Email = "U1@example.com",
    Password = "password123",
- Setelah login, kamu akan diarahkan ke halaman daftar produk.

#Catatan Tambahan
File upload disimpan di folder wwwroot/uploads
Soft delete hanya menandai data, tidak benar-benar menghapus dari database

🙋‍♀️ Author
Annisa Aulia Wahyudi – https://github.com/annisauliawahyudi
