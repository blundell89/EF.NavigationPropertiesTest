using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EF.NavigationPropertiesTest.App;

public class PolicyContext : DbContext
{
    public DbSet<Policy> Policies { get; private set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.LogTo(Console.WriteLine);
        optionsBuilder.UseSqlServer(
            "Server=localhost,1434;Database=NavigationPropertiesTest;Trusted_Connection=True;Integrated Security=False;User Id=sa;Password=dsfosdfkdfsJdsfsdf^0;TrustServerCertificate=True");
        optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        optionsBuilder.EnableSensitiveDataLogging();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Policy>(builder =>
        {
            builder.ToTable("Policies");

            builder.HasKey(x => x.PolicyId);
            
            builder.Property(x => x.PolicyId)
                .ValueGeneratedNever();
        });

        modelBuilder.Entity<PolicyEvent>(builder =>
        {
            builder.ToTable("PolicyEvents");

            builder.HasKey(x => new { x.PolicyId, x.UpdatedAt });

            builder.HasOne(x => x.Policy)
                .WithMany(x => x.Events)
                .HasForeignKey(x => x.PolicyId);
            
            builder.Property(x => x.Event).HasMaxLength(200);
        });
    }
}

public record Policy(Guid PolicyId)
{
    public List<PolicyEvent> Events { get; private set; } = new();
    
    public void AddEvent(string policyEvent) => Events.Add(new(PolicyId, policyEvent));
}

public record PolicyEvent(Guid PolicyId, string Event)
{
    public Policy Policy { get; private set; } = null!;
    public DateTime UpdatedAt { get; private set; } = DateTime.UtcNow;
};