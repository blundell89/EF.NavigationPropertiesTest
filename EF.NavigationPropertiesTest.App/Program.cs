// See https://aka.ms/new-console-template for more information

using EF.NavigationPropertiesTest.App;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

var services = new ServiceCollection();

services
    .AddLogging(builder =>
    {
        builder.ClearProviders();
        builder.SetMinimumLevel(LogLevel.Debug);
        builder.AddJsonConsole();
    });

services
    .AddEntityFrameworkSqlServer()
    .AddDbContext<PolicyContext>(ServiceLifetime.Singleton);

var provider = services.BuildServiceProvider();

var context = provider.GetRequiredService<PolicyContext>();
await context.Database.MigrateAsync();

var createdPolicyId = await SeedPolicy();

var policies = await context.Policies
    .Where(x => x.PolicyId == createdPolicyId)
    .Select(pol => new
    {
        Policy = pol,
        CreatedEvent = pol.Events
            .OrderByDescending(x => x.UpdatedAt)
            .FirstOrDefault(x => x.Event == "Created")
    })
    .ToListAsync();

foreach (var policy in policies)
{
    Console.WriteLine($"Policy: {policy.Policy.PolicyId}. Latest event: {policy.CreatedEvent!.Event}");
}

async Task<Guid> SeedPolicy()
{
    var policy = new Policy(Guid.NewGuid());
    policy.AddEvent("Created");
    policy.AddEvent("Updated");

    context.Policies.Add(policy);
    await context.SaveChangesAsync();

    return policy.PolicyId;
}