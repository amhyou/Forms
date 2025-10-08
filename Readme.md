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


Example environment variables:

```text
Postgres: Host=postgres-hl.postgres;Port=5432;Database=forms;Username=amhyou;Password=supersecret123
Redis: redisearch.redisearch:6379
```