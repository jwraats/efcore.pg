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
        var tableCount = await context.Database.SqlQueryRaw<int>(
            "SELECT COUNT(*) FROM information_schema.tables WHERE table_name = 'Employees'").FirstOrDefaultAsync();
        
        Assert.Equal(1, tableCount);

        // Verify that the history table exists
        var historyTableCount = await context.Database.SqlQueryRaw<int>(
            "SELECT COUNT(*) FROM information_schema.tables WHERE table_name = 'EmployeesHistory'").FirstOrDefaultAsync();
        
        Assert.Equal(1, historyTableCount);
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
        var historyCount = await rawContext.Database.SqlQueryRaw<int>(
            "SELECT COUNT(*) FROM \"EmployeesHistory\" WHERE \"Id\" = {0}", employeeId).FirstOrDefaultAsync();

        // The history trigger should have created a record
        Assert.True(historyCount > 0, "Expected at least one history record to be created");
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
