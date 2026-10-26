# ภูมิเมล (Email Signup)

แอปสมัครรับข่าวสารทางอีเมล พร้อมเลือกภูมิภาค — ASP.NET Core 10 + SQLite

## โครงสร้าง

```
EmailSignup.slnx
src/
  EmailSignup.Domain/          # Entities, Enums
  EmailSignup.Application/     # Interfaces, Services, DTOs
  EmailSignup.Infrastructure/  # EF Core + SQLite, Repositories
  EmailSignup.Web/             # API + หน้าเว็บ
```

## รันโปรเจกต์

```bash
dotnet run --project src/EmailSignup.Web
```

เปิดเบราว์เซอร์ที่ URL ที่แสดงในคอนโซล (เช่น `http://localhost:5xxx`)

## API

| Method | Path | คำอธิบาย |
|--------|------|----------|
| GET | `/api/regions` | รายการภูมิภาค |
| POST | `/api/subscribe` | สมัครรับข่าว |

ตัวอย่าง body:

```json
{
  "displayName": "สมชาย",
  "email": "somchai@example.com",
  "region": 1
}
```

ค่า `region`: 1 เหนือ · 2 อีสาน · 3 กลาง · 4 ตะวันออก · 5 ตะวันตก · 6 ใต้ · 7 ต่างประเทศ
