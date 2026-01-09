# Temporal Tables in Npgsql Entity Framework Core Provider

This document explains how to use temporal tables in the Npgsql EF Core provider for PostgreSQL 18.

## What are Temporal Tables?

Temporal tables are a database feature that automatically tracks the history of data changes. When you update or delete a row, the old version is automatically saved to a history table, allowing you to query data "as it was" at any point in time.

## How it Works in PostgreSQL

Since PostgreSQL 18 doesn't have native SQL:2011 temporal table support built-in, this implementation uses:
- A **history table** with the same structure as the main table
- A **trigger function** that automatically copies old row versions to the history table on UPDATE/DELETE
- A **trigger** on the main table that invokes the function

## Usage

### Basic Configuration

To configure an entity as a temporal table, use the `IsTemporal()` method in your model configuration:

```csharp
public class BlogContext : DbContext
{
    public DbSet<Blog> Blogs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Blog>(b =>
        {
            b.IsTemporal();
        });
    }
}

public class Blog
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    
    // Period columns for tracking
    public DateTime PeriodStart { get; set; }
    public DateTime PeriodEnd { get; set; }
}
```

### Custom Configuration

You can customize the history table name and period column names:

```csharp
modelBuilder.Entity<Blog>(b =>
{
    b.IsTemporal(temporal: true, buildAction: ttb =>
    {
        ttb.UseHistoryTable("BlogHistory", "audit");
        ttb.HasPeriod("ValidFrom", "ValidTo");
    });
});
```

## Migration

When you create a migration for a temporal table, the provider will generate:

1. The main table with your entity's columns
2. A history table with the same structure
3. A trigger function to maintain the history
4. A trigger on the main table

Example generated SQL:

```sql
CREATE TABLE "Blogs" (
    "Id" integer NOT NULL,
    "Title" text NOT NULL,
    "Content" text NOT NULL,
    "PeriodStart" timestamp with time zone NOT NULL,
    "PeriodEnd" timestamp with time zone NOT NULL,
    PRIMARY KEY ("Id")
);

CREATE TABLE "BlogsHistory" (
    "Id" integer NOT NULL,
    "Title" text NOT NULL,
    "Content" text NOT NULL,
    "PeriodStart" timestamp with time zone NOT NULL,
    "PeriodEnd" timestamp with time zone NOT NULL
);

CREATE OR REPLACE FUNCTION "public_Blogs_history_trigger"()
RETURNS TRIGGER AS $$
BEGIN
    IF (TG_OP = 'DELETE') THEN
        INSERT INTO "BlogsHistory" SELECT OLD.*;
        RETURN OLD;
    ELSIF (TG_OP = 'UPDATE') THEN
        INSERT INTO "BlogsHistory" SELECT OLD.*;
        RETURN NEW;
    END IF;
    RETURN NULL;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER "Blogs_history_trigger"
    AFTER UPDATE OR DELETE ON "Blogs"
    FOR EACH ROW
    EXECUTE FUNCTION "public_Blogs_history_trigger"();
```

## Querying Historical Data

While the history table is automatically maintained, you can query it directly to retrieve historical data:

```csharp
// Get all historical versions of a specific blog
var history = await context.Database
    .SqlQuery<Blog>($"SELECT * FROM \"BlogsHistory\" WHERE \"Id\" = {blogId}")
    .ToListAsync();

// Get data as it was at a specific point in time
var pointInTime = DateTime.UtcNow.AddDays(-7);
var historicalBlog = await context.Database
    .SqlQuery<Blog>($"SELECT * FROM \"BlogsHistory\" WHERE \"Id\" = {blogId} AND \"PeriodStart\" <= {pointInTime} AND \"PeriodEnd\" > {pointInTime}")
    .FirstOrDefaultAsync();
```

## Limitations

- PostgreSQL 18 does not have native SQL:2011 temporal table syntax, so you cannot use `FOR SYSTEM_TIME AS OF` queries directly
- The period columns (PeriodStart/PeriodEnd) must be managed by your application
- History tables are separate tables that you query independently

## Future Enhancements

Potential future improvements could include:
- Extension methods for temporal queries (TemporalAsOf, TemporalBetween, etc.)
- Automatic management of period columns
- Integration with PostgreSQL 18+ when native temporal table support is added
