//using Blazored.LocalStorage;
//using Client_Interface_Eventit;
//using Microsoft.AspNetCore.Components.Web;
//using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
//using Microsoft.Extensions.DependencyInjection;
//using Services.Implementations;
//using Client_Interface_Eventit.ApiClient;
//using System.Net.Http;
//using Services.Interfaces;

//var builder = WebAssemblyHostBuilder.CreateDefault(args);
//builder.RootComponents.Add<App>("#app");
//builder.RootComponents.Add<HeadOutlet>("head::after");

//builder.Services.AddBlazoredLocalStorage();

////builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7279/") });
//builder.Services.AddScoped(sp =>
//{
//    var httpClient = new HttpClient
//    {
//        BaseAddress = new Uri("https://localhost:7279/")
//    };

//    var token = sp.GetRequiredService<ILocalStorageService>()
//                  .GetItemAsync<string>("Token")
//                  .Result;

//    if (!string.IsNullOrEmpty(token))
//    {
//        httpClient.DefaultRequestHeaders.Authorization =
//            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
//    }

//    return httpClient;
//});

//builder.Services.AddScoped<ICommentManager, CommentManager>();

////builder.Services.AddScoped<IClient>(sp =>
////{
////    var httpClient = sp.GetRequiredService<HttpClient>();
////    var baseUrl = "https://localhost:7279"; 
////    return new Client(baseUrl, httpClient); 
////});

//builder.Services.AddTransient<AuthHeaderHandler>();

//builder.Services.AddHttpClient<IClient, Client>(client =>
//{
//    client.BaseAddress = new Uri("https://localhost:7279/");
//})
//.AddHttpMessageHandler<AuthHeaderHandler>();



//builder.Services.AddAuthorizationCore();

//await builder.Build().RunAsync();

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
    BaseAddress = new Uri("https://localhost:7279/")
});

builder.Services.AddScoped<IClient>(sp =>
{
    var httpClient = sp.GetRequiredService<HttpClient>();
    return new Client("https://localhost:7279", httpClient);
});

builder.Services.AddScoped<ICommentManager, CommentManager>();

builder.Services.AddAuthorizationCore();

await builder.Build().RunAsync();

