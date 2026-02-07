MoneyBee - Microservices

🏗 Mimari Özellikler
Framework: .NET 8

Pattern: CQRS & MediatR

Architecture: Onion (Clean) Architecture

Database: PostgreSQL & EF Core

Orchestration: Docker Compose

📂 Proje Yapısı
src/Services/MoneyBee.Customer: Müşteri yönetimi, KYC doğrulama entegrasyonu ve limit takibi.

src/Services/MoneyBee.Transfer: Para transferi, bakiye yönetimi ve işlem geçmişi.

src/BuildingBlocks/MoneyBee.Shared: Ortak modeller, Global Exception Handling ve ServiceResponse yapısı.

External Mocks: KYC (8082), Fraud (8081) ve Exchange (8083) servis simülatörleri.

🚀 Kurulum ve Çalıştırma
1. Sistemi Başlat

Proje ana dizininde aşağıdaki komutu çalıştırarak tüm servisleri ve bağımlılıkları (DB, Redis) ayağa kaldırın:

Bash
docker-compose up -d --build
2. Veritabanı Güncelleme (Migration)

Servisler ayağa kalktıktan sonra tabloların oluşması için migration komutlarını çalıştırın:

Customer Service:

Bash
dotnet ef database update --project src/Services/MoneyBee.Customer/MoneyBee.Customer.Infrastructure --startup-project src/Services/MoneyBee.Customer/MoneyBee.Customer.API
Transfer Service:

Bash
dotnet ef database update --project src/Services/MoneyBee.Transfer/MoneyBee.Transfer.Infrastructure --startup-project src/Services/MoneyBee.Transfer/MoneyBee.Transfer.API

🧪 Servis Adresleri ve Portlar
Customer API: http://localhost:5001/swagger

Transfer API: http://localhost:5002/swagger

KYC Mock Service: http://localhost:8082

Fraud Mock Service: http://localhost:8081

Exchange Mock Service: http://localhost:8083

PostgreSQL: localhost:5432

🛠 Test Akışı
Müşteri Kaydı: customer-service üzerinden POST isteği atılır. Sistem external-kyc üzerinden kimlik doğrulaması yapar.

Hesap Açılışı: Kayıt başarılıysa müşteriye otomatik olarak bir Account ve CustomerLimit tanımlanır.

Para Transferi: transfer-service üzerinden gönderim başlatılır. Sistem Fraud kontrolü yapar ve Exchange servisi üzerinden kur çevrimini hesaplar.