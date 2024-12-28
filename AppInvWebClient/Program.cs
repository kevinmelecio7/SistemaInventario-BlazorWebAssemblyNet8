using AppInvWebClient;
using AppInvWebClient.ClientServices;
using AppInvWebClient.ClientStates;
using AppInvWebClient.Modals;
using AppInvWebSharedLibrary.Contracts;
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Radzen;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });



builder.Services.AddScoped<IAccount, AccountService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<BitacoraService>();
builder.Services.AddScoped<InputsDataService>();
builder.Services.AddScoped<ILoading, Loading>();
builder.Services.AddScoped<IModal, Modal>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
builder.Services.AddScoped<TokenService>();

builder.Services.AddAuthorizationCore();
builder.Services.AddBlazoredSessionStorage();
builder.Services.AddRadzenComponents();

await builder.Build().RunAsync();
