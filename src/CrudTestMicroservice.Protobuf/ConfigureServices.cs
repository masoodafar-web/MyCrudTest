using FluentValidation;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddProtobufServices(this IServiceCollection services)
    {        
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());        
        return services;
    }    
}

