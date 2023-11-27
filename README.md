# Getting Started

1. Run `docker compose up -d`. This will start SQL server on port 1434.
2. Run the app `dotnet run --project EF.NavigationPropertiesTest.App/EF.NavigationPropertiesTest.App.csproj`
3. Examine the SQL output:

```sql
SELECT [p].[PolicyId], [t0].[PolicyId], [t0].[UpdatedAt[t0].[Event]
FROM [Policies] AS [p]
LEFT JOIN (
    SELECT [t].[PolicyId], [t].[UpdatedAt], [t].[Event]
    FROM (
        SELECT [p0].[PolicyId], [p0].[UpdatedAt], [p[Event], ROW_NUMBER() OVER(PARTITION BY [p[PolicyId] ORDER BY [p0].[UpdatedAt] DESC) AS [row]
        FROM [PolicyEvents] AS [p0]
        WHERE [p0].[Event] = N'Created'
    ) AS [t]
    WHERE [t].[row] <= 1
) AS [t0] ON [p].[PolicyId] = [t0].[PolicyId]
WHERE [p].[PolicyId] = @__createdPolicyId_0
```