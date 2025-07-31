Bu proje **.Net Core** ve **PostgreSQL** ile **OrderService**, **StockService** ve **NotificationService** mikroservislerinin **RabbitMQ Event Driven Communication** üzerinden haberleştiği bir yapı ile hazırlanmıştır. 
Dağıtık bir sistemde mesajlaşma ve veri yönetimi konularındaki çalışmaları kapsamaktadır.

#### OrderService Mikroservisi Aşağıdaki Maddeleri İçermektedir;
* .Net Core Web API Application
* Layered Architecture
* PostgreSQL veritabanı Bağlantısı
* Generic Repository İmplementasyonu
* Swagger Open API İmplementasyonu
* FluentValidation İmplementasyonu
* RabbitMQ Kullanarak stock-queue Publishi
* RabbitMQ Kullanarak notification-queue Publishi
* Exception Middleware
* Validation Middleware

#### StockService Mikroservisi Aşağıdaki Maddeleri İçermektedir;
* .Net Core Web API Application
* Layered Architecture
* PostgreSQL Veritabanı Bağlantısı
* Generic Repository İmplementasyonu
* FluentValidation İmplementasyonu
* Swagger Open API İmplementasyonu
* Exception Middleware
* Validation Middleware
* Loglama Altyapısı

#### NotificationService Mikroservisi Aşağıdaki Maddeleri İçermektedir;
* .Net Core Web API Application
* Layered Architecture
* PostgreSQL Veritabanı Bağlantısı
* Generic Repository İmplementasyonu
* FluentValidation İmplementasyonu
* Swagger Open API İmplementasyonu
* Loglama Altyapısı
* Exception Middleware

#### NotificationService.WorkerService Aşağıdaki Maddeleri İçermektedir;
* .NET Core Worker Service Application
* Retry Pattern
* RabbitMQ Kullanarak notification-queue consume'u

#### StockService.WorkerService Aşağıdaki Maddeleri İçermektedir;
* .NET Core Worker Service Application
* Retry Pattern
* RabbitMQ Client


## ⚙️ Başlatma Adımları

1. Bilgisayarınızda Docker ve Docker Compose yüklü olduğundan emin olun
2. Projeyi klonlayın
3. docker-compose up -d ile servisleri başlatın


  
  
  
