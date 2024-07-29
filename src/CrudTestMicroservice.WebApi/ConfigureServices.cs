using CrudTestMicroservice.Application.Common.Interfaces;
using CrudTestMicroservice.Infrastructure.Persistence;
using CrudTestMicroservice.WebApi.Common.Services;
using MapsterMapper;
using System.Reflection;
using CrudTestMicroservice.WebApi.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddPresentationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMapping();

        services.AddDatabaseDeveloperPageExceptionFilter();

        services.AddScoped<ICurrentUserService, CurrentUserService>();
        services.AddScoped<IDispatchRequestToCQRS, DispatchRequestToCQRS>();

        services.AddHttpContextAccessor();

        services.AddHealthChecks()
            .AddDbContextCheck<ApplicationDbContext>();

        return services;
    }
    private static IServiceCollection AddMapping(this IServiceCollection services)
    {
        var typeAdapterConfig = TypeAdapterConfig.GlobalSettings;
        // scans the assembly and gets the IRegister, adding the registration to the TypeAdapterConfig
        typeAdapterConfig.Scan(Assembly.GetExecutingAssembly());
        // register the mapper as Singleton service for my application
        var mapperConfig = new Mapper(typeAdapterConfig);
        services.AddSingleton<IMapper>(mapperConfig);
        return services;
    }
    // get all grpc endpoint that end with "Service" in specified assembly and register them as grpc client
    public static WebApplication ConfigureGrpcEndpoints(this WebApplication app, Assembly? assembly = null,
        Action<IEndpointRouteBuilder>? configure = null)
    {
        if (assembly is not null)
        {
            var assemblyName = assembly.GetName().Name;
            var grpcServices = assembly.GetTypes()
                // check name and type
                .Where(t => t.Name.EndsWith("Service") && t.IsClass)
                // check folder by assembly qualified name
                .Where(t => t.AssemblyQualifiedName != null &&
                            t.AssemblyQualifiedName.Contains($"{assemblyName}.Services"))
                // check parent name ends with "ContractBase"
                .Where(t => t.BaseType?.Name.EndsWith("ContractBase") == true)
                .ToList();

            app.UseEndpoints(endpoints =>
            {
                foreach (var service in grpcServices)
                {
                    // how to use type as generic parameter in csharp?
                    // https://stackoverflow.com/questions/3957817/calling-generic-method-with-type-variable
                    var method = typeof(GrpcEndpointRouteBuilderExtensions).GetMethod("MapGrpcService");
                    var generic = method?.MakeGenericMethod(service);
                    generic?.Invoke(null, new object[] { endpoints });
                }
            });
        }

        if (configure is not null)
        {
            app.UseEndpoints(configure.Invoke);
        }

        return app;
    }
}
