using CrudTestMicroservice.Application.Common.Interfaces;
using CrudTestMicroservice.Infrastructure.Persistence;
using CrudTestMicroservice.Infrastructure.Persistence.Interceptors;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<AuditableEntitySaveChangesInterceptor>();
        services.AddScoped<ApplicationDbContextInitialiser>();
        services.AddScoped<IApplicationDbContext>(p => p.GetRequiredService<ApplicationDbContext>());
        if (configuration.GetValue<bool>("UseInMemoryDatabase"))
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseInMemoryDatabase("MyMemoryDb"));
        }
        else
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                    builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
        }
        #region AddAuthentication

        var message = "";
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(jwtBearerOptions =>
            {
                jwtBearerOptions.Authority = configuration["Authentication:Authority"];
                jwtBearerOptions.Audience = configuration["Authentication:Audience"];
                jwtBearerOptions.TokenValidationParameters.ValidateAudience = false;                
                jwtBearerOptions.TokenValidationParameters.ValidateIssuer = true;
                jwtBearerOptions.TokenValidationParameters.ValidateIssuerSigningKey = false;
                try
                {
                    jwtBearerOptions.Events = new JwtBearerEvents
                    {
                        OnAuthenticationFailed = ctx =>
                        {
                            ctx.Response.StatusCode = StatusCodes.Status401Unauthorized;
                            message += "From OnAuthenticationFailed:\n";
                            message += ctx.Exception.Message;
                            return Task.CompletedTask;
                        },

                        OnChallenge = ctx =>
                        {
                            message += "From OnChallenge:\n";
                            ctx.Response.StatusCode = StatusCodes.Status401Unauthorized;
                            ctx.Response.ContentType = "text/plain";
                            return ctx.Response.WriteAsync(message);
                        },

                        OnMessageReceived = ctx =>
                        {
                            message = "From OnMessageReceived:\n";
                            ctx.Request.Headers.TryGetValue("Authorization", out var BearerToken);
                            if (BearerToken.Count == 0)
                                BearerToken = "no Bearer token sent\n";
                            message += "Authorization Header sent: " + BearerToken + "\n";
                            return Task.CompletedTask;
                        },

                        OnTokenValidated = ctx =>
                        {
                            Debug.WriteLine("token: " + ctx.SecurityToken.ToString());
                            return Task.CompletedTask;
                        }
                    };
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            });

        services.AddAuthorization();

        #endregion

        return services;
    }
}