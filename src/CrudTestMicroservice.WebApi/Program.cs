using CrudTestMicroservice.Infrastructure.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting;
using Serilog.Core;
using Serilog;
using System.Reflection;
using Microsoft.OpenApi.Models;
using CrudTestMicroservice.WebApi.Common.Behaviours;

var builder = WebApplication.CreateBuilder(args);
var logger = new LoggerConfiguration()
    //.WriteTo.Console()
    /*.WriteTo.MSSqlServer(builder.Configuration.GetConnectionString("LogConnection"),
        sinkOptions: new MSSqlServerSinkOptions
        {
            TableName = "LogCrudTestEvents",
            SchemaName = "Log",
            AutoCreateSqlTable = true
        })
    .WriteTo.Seq("http://localhost:5341",
                  apiKey: "IeEfKjIMoCGLljdp9e7A",
                  controlLevelSwitch: new LoggingLevelSwitch(Serilog.Events.LogEventLevel.Information))*/
    .CreateLogger();
builder.Logging.AddSerilog(logger);
#if DEBUG
Serilog.Debugging.SelfLog.Enable(msg => Console.WriteLine(msg));
#endif

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

// Add services to the container.

builder.Services.AddGrpc(options =>
{
    options.Interceptors.Add<LoggingBehaviour>();
    options.Interceptors.Add<PerformanceBehaviour>();
    options.EnableDetailedErrors = true;
    options.MaxReceiveMessageSize = 1000 * 1024 * 1024; // 1 GB
    options.MaxSendMessageSize = 1000 * 1024 * 1024; // 1 GB
}).AddJsonTranscoding();
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddPresentationServices(builder.Configuration);
builder.Services.AddProtobufServices();

#region Configure Cors

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().WithExposedHeaders("Grpc-Status",
            "Grpc-Message", "Grpc-Encoding", "Grpc-Accept-Encoding", "validation-errors-text"));
});

#endregion
builder.Services.AddGrpcSwagger();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "gRPC transcoding", Version = "v1" });
    c.CustomSchemaIds(type => type.ToString());
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please insert JWT with Bearer into field",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseMigrationsEndPoint();

    // Initialise and seed database
    using (var scope = app.Services.CreateScope())
    {
        var initialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();
        await initialiser.InitialiseAsync();
        await initialiser.SeedAsync();
    }
}
else
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseRouting();
app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();
app.UseGrpcWeb(new GrpcWebOptions { DefaultEnabled = true }); // Configure the HTTP request pipeline.
app.ConfigureGrpcEndpoints(Assembly.GetExecutingAssembly(), endpoints =>
{
    // endpoints.MapGrpcService<ExampleService>();
});

app.MapGet("/",   context =>
{
   return  Task.Run(()=>context.Response.Redirect("/swagger/index.html"));
});
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
});
app.Run();
public partial class Program { }