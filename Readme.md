# Forms

The **Forms** project lets you create templates for forms to be shared and filled with anyone, and see the results.

## Deployment Diagram

![Deployment Diagram](https://media.amhyou.com/Forms.jpg)

## Conceptual Model Diagram

![Conceptual Model Diagram](https://media.amhyou.com/Models.jpeg)

## Prerequisites & Configuration

Use `redis-cli` to create indexes and seed initial data:

```bash
# Redisearch indexes
FT.CREATE idx_tags ON HASH PREFIX 1 tag: SCHEMA Id NUMERIC NOINDEX Name TAG
FT.CREATE idx_users ON HASH PREFIX 1 user: SCHEMA Id TEXT NOINDEX Email TEXT
FT.CREATE idx_search ON HASH PREFIX 2 comment:question: SCHEMA Id NUMERIC NOINDEX Value TEXT
FT.CREATE idx_topics ON HASH PREFIX 1 topic: SCHEMA Id NUMERIC NOINDEX Name TAG

# Seed topics
hset topic:1 Id "1" Name "Math"
hset topic:2 Id "2" Name "Physics"
hset topic:3 Id "3" Name "Music"
hset topic:4 Id "4" Name "Sport"
hset topic:5 Id "5" Name "Other"
```

Install EF Core CLI (if not installed):

```bash
dotnet new tool-manifest
dotnet tool install dotnet-ef
```

Apply database migrations:

```bash
dotnet ef database update
```

Seed initial topics in PostgreSQL:

```bash
psql -U amhyou -d forms -c "
INSERT INTO \"Topics\" (\"Id\", \"Name\") VALUES
(1, 'Math'),
(2, 'Physics'),
(3, 'Music'),
(4, 'Sport'),
(5, 'Other');
"
```


MinIO configuration and setup:

```bash
# Configure MinIO alias
mc alias set myminio https://cdn.amhyou.com minioadmin supersecret123 --api s3v4

# Create bucket for media files
mc mb myminio/forms-media
mc ls myminio

# Create a user and attach read/write policy
mc admin user add myminio jY5RJpw1ltEofOy61SyO Vq8uLdGy7gH5fglNdiyjzlwodOJiNbDMoYN4S6g0
mc admin policy attach myminio readwrite --user jY5RJpw1ltEofOy61SyO
```


## Example of Environment Variables

Before using the app, make sure you have an **Azure AD app registration** set up:

1. Go to the [Azure Portal](https://portal.azure.com/) → **Azure Active Directory → App registrations → New registration**.
2. Set a **Name** for your app and select the supported account type.
3. After creating, copy the **Application (client) ID** (`ClientId`).
4. Go to **Certificates & secrets → Client secrets → + New client secret** and generate a new secret. Copy its value — this is your `ClientSecret`.
5. Optionally configure **Redirect URIs** and **API permissions**, and grant admin consent if needed.
6. Keep these values safe; you will use them in your environment configuration.

Once you have these, you can configure your application using environment variables or `appsettings.json` like this:

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "Postgres": "Host=postgres-hl.postgres;Port=5432;Database=forms;Username=amhyou;Password=AmhYou85231!",
    "Redis": "redisearch.redisearch:6379"
  },
  "Minio": {
    "Endpoint": "cdn.amhyou.com",
    "AccessKey": "jY5RJpw1ltEofOy61SyO",
    "SecretKey": "Vq8uLdGy7gH5fglddiyjzlwozOJiNbvFoYN4S6g0",
    "BucketName": "forms-media"
  },
  "EmailSettings": {
    "SmtpServer": "smtp.mailersend.net",
    "SmtpPort": 587,
    "SmtpUsername": "MS_ZajJCI@amhyou.guru",
    "SmtpPassword": "mssp.DzFxHar.jpzkugqqxv2y059v.hbsyDiJ",
    "SenderEmail": "info@amhyou.guru",
    "SenderName": "Forms"
  },
  "AzureAd": {
    "ClientId": "83c403d3-f844-4576-9a93-2543da3d228c",
    "ClientSecret": "6cMmQ~ZL0aH9lNvjoSsfc6rvQzalRrZGqfNxJceW"
  }
}
```