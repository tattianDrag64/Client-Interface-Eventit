

using Blazored.LocalStorage;
using Client_Interface_Eventit;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Services.Implementations;
using Client_Interface_Eventit.ApiClient;
using Services.Interfaces;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddBlazoredLocalStorage();

builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("https://localhost:7279/"), 

});

builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("https://localhost:7299/"),

});

builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("https://localhost:7264/"),

});

builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("https://localhost:7277/"),

});


//NSwag installing 



builder.Services.AddScoped<IClient>(sp =>
{
    var httpClient = sp.GetRequiredService<HttpClient>();
    return new Client("https://localhost:7279", httpClient);
});

builder.Services.AddScoped<IClientEvent>(sp =>
{
    var httpClient = sp.GetRequiredService<HttpClient>();
    return new ClientEvent("https://localhost:7299", httpClient);
});

builder.Services.AddScoped<IClientUser>(sp =>
{
    var httpClient = sp.GetRequiredService<HttpClient>();
    return new ClientUser("https://localhost:7264", httpClient);
});

builder.Services.AddScoped<IClientTask>(sp =>
{
    var httpClient = sp.GetRequiredService<HttpClient>();
    return new ClientTask("https://localhost:7277", httpClient);
});


builder.Services.AddScoped<ICommentManager, CommentManager>();

builder.Services.AddAuthorizationCore();

await builder.Build().RunAsync();

