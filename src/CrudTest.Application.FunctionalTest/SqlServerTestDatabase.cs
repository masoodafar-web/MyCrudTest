using System.Data.Common;
using CrudTestMicroservice.Domain.Common.Guards;
using CrudTestMicroservice.Infrastructure.Persistence;
using CrudTestMicroservice.Infrastructure.Persistence.Interceptors;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Respawn;

namespace CrudTest.Application.FunctionalTest;

public class SqlServerTestDatabase : ITestDatabase
{
    private readonly string _connectionString = null!;
    private SqlConnection _connection = null!;
    private Respawner _respawner = null!;

    public SqlServerTestDatabase()
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddEnvironmentVariables()
            .Build();

        var connectionString = configuration.GetConnectionString("DefaultConnection");

        Guard.Against.NullOrEmpty(connectionString);

        _connectionString = connectionString;
    }

    public async Task InitialiseAsync()
    {
        _connection = new SqlConnection(_connectionString);

        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseSqlServer(_connectionString)
            .Options;

        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddEnvironmentVariables()
            .Build();
        
        var provider = new ServiceCollection().AddPresentationServices(configuration).BuildServiceProvider();
        var auditableEntitySaveChangesInterceptor=provider.GetService<AuditableEntitySaveChangesInterceptor>();
        var mediator=provider.GetService<IMediator>();
        var context = new ApplicationDbContext(options,mediator,auditableEntitySaveChangesInterceptor);

        context.Database.Migrate();

        _respawner = await Respawner.CreateAsync(_connectionString, new RespawnerOptions
        {
            TablesToIgnore = new Respawn.Graph.Table[] { "__EFMigrationsHistory" }
        });
    }

    public DbConnection GetConnection()
    {
        return _connection;
    }

    public async Task ResetAsync()
    {
        await _respawner.ResetAsync(_connectionString);
    }

    public async Task DisposeAsync()
    {
        await _connection.DisposeAsync();
    }
}
