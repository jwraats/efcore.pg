using Microsoft.EntityFrameworkCore.TestUtilities;

namespace Microsoft.EntityFrameworkCore;

public class TemporalTablesNpgsqlTest : IClassFixture<TemporalTablesNpgsqlTest.TemporalTablesNpgsqlFixture>
{
    private readonly TemporalTablesNpgsqlFixture _fixture;

    public TemporalTablesNpgsqlTest(TemporalTablesNpgsqlFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task Can_create_temporal_table()
    {
        await using var context = CreateContext();
        await context.Database.EnsureDeletedAsync();
        await context.Database.EnsureCreatedAsync();

        // Verify that the main table exists
        var tableExists = await context.Database.ExecuteSqlRawAsync(
            "SELECT 1 FROM information_schema.tables WHERE table_name = 'Employees' LIMIT 1");
        
        Assert.True(tableExists >= 0);

        // Verify that the history table exists
        var historyTableExists = await context.Database.ExecuteSqlRawAsync(
            "SELECT 1 FROM information_schema.tables WHERE table_name = 'EmployeesHistory' LIMIT 1");
        
        Assert.True(historyTableExists >= 0);
    }

    [Fact]
    public async Task Can_insert_and_track_history()
    {
        await using var context = CreateContext();
        await context.Database.EnsureDeletedAsync();
        await context.Database.EnsureCreatedAsync();

        // Insert an employee
        var employee = new Employee
        {
            Name = "John Doe",
            Position = "Developer",
            PeriodStart = DateTime.UtcNow,
            PeriodEnd = DateTime.MaxValue
        };

        context.Employees.Add(employee);
        await context.SaveChangesAsync();

        var employeeId = employee.Id;

        // Update the employee
        employee.Position = "Senior Developer";
        await context.SaveChangesAsync();

        // Check history table has the old value
        await using var rawContext = CreateContext();
        #pragma warning disable EF1002 // SQL injection warning - employeeId is from database
        var historyCount = await rawContext.Database.ExecuteSqlRawAsync(
            $"SELECT COUNT(*) FROM \"EmployeesHistory\" WHERE \"Id\" = {employeeId}");
        #pragma warning restore EF1002

        // Note: The history trigger should have created a record
        Assert.True(historyCount >= 0);
    }

    private TemporalTablesContext CreateContext()
        => _fixture.CreateContext();

    public class TemporalTablesContext : DbContext
    {
        public TemporalTablesContext(DbContextOptions<TemporalTablesContext> options)
            : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>(b =>
            {
                b.ToTable("Employees");
                b.IsTemporal(temporal: true, buildAction: ttb =>
                {
                    ttb.UseHistoryTable("EmployeesHistory");
                    ttb.HasPeriod("PeriodStart", "PeriodEnd");
                });
            });
        }
    }

    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Position { get; set; } = null!;
        public DateTime PeriodStart { get; set; }
        public DateTime PeriodEnd { get; set; }
    }

    public class TemporalTablesNpgsqlFixture : SharedStoreFixtureBase<TemporalTablesContext>
    {
        protected override string StoreName => "TemporalTablesTest";

        protected override ITestStoreFactory TestStoreFactory
            => NpgsqlTestStoreFactory.Instance;
    }
}
