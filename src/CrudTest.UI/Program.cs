using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using CrudTest.UI;
using CrudTestMicroservice.Protobuf.Protos.Customer;
using Grpc.Core;
using Grpc.Net.Client;
using Grpc.Net.Client.Web;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddMudServices();

var baseUrl = builder.Configuration["GwUrl"];
var httpClient = new HttpClient(new GrpcWebHandler(GrpcWebMode.GrpcWeb, new HttpClientHandler()));
httpClient.Timeout = TimeSpan.FromMinutes(10); // TODO Check Timeout
var serviceProvider = builder.Services.BuildServiceProvider();
var channel = CreateAuthenticatedChannel(baseUrl, httpClient, serviceProvider);
builder.Services.AddSingleton(sp => new CustomerContract.CustomerContractClient(channel));


await builder.Build().RunAsync();

static GrpcChannel CreateAuthenticatedChannel(string address, HttpClient httpClient, IServiceProvider serviceProvider)
{
    var credentials = CallCredentials.FromInterceptor(async (context, metadata) =>
    {
        /*var provider = serviceProvider.GetRequiredService<IAccessTokenProvider>();
        var accessToken = await provider.RequestAccessToken();
        accessToken.TryGetToken(out var token);
        if (token != null && !string.IsNullOrEmpty(token.Value))
        {
            // Console.WriteLine($"Authorization Bearer {token.Value}");
            metadata.Add("Authorization", $"Bearer {token.Value}");
        }
        */

        await Task.CompletedTask;
    });

    // SslCredentials is used here because this channel is using TLS.
    // CallCredentials can't be used with ChannelCredentials.Insecure on non-TLS channels.
    var channel = GrpcChannel.ForAddress(address, new GrpcChannelOptions
    {
        UnsafeUseInsecureChannelCallCredentials = true,
        Credentials = ChannelCredentials.Create(new SslCredentials(), credentials),
        HttpClient = httpClient,
        MaxReceiveMessageSize = 1000 * 1024 * 1024, // 1 GB
        MaxSendMessageSize = 1000 * 1024 * 1024 // 1 GB
    });
    return channel;
}