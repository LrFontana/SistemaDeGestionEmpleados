using Blazored.LocalStorage;
using Client;
using Client.EstadosDeLaAplicacion;
using LibreriaBase.Entidades;
using LibreriaCliente.Helpers;
using LibreriaCliente.Servicios.Contratos;
using LibreriaCliente.Servicios.Implementaciones;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Syncfusion.Blazor;
using Syncfusion.Blazor.Popups;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddTransient<HandlerHttpPersonalizado>();
builder.Services.AddHttpClient("SystemApiClient", client =>
{
    client.BaseAddress = new Uri("https://localhost:7148");

}).AddHttpMessageHandler<HandlerHttpPersonalizado>(); ;

//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7148/") });
builder.Services.AddAuthorizationCore();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<ObtenerHttpCliente>();
builder.Services.AddScoped<ServicioDeAlmacenamientoLocal>();
builder.Services.AddScoped<AuthenticationStateProvider, ProveedorDeEstadoDeAutenticacionPersonalizado>();
builder.Services.AddScoped<IServicioCuentaUsuario, ServicioCuentaUsuario>();

// Departamento General / Departamento / Rama
builder.Services.AddScoped<IServicioInterfazGenerica<DepartamentoGeneral>, ServicioInerfazGenerica<DepartamentoGeneral>>();
builder.Services.AddScoped<IServicioInterfazGenerica<Departamento>, ServicioInerfazGenerica<Departamento>>();
builder.Services.AddScoped<IServicioInterfazGenerica<Rama>, ServicioInerfazGenerica<Rama>>();

// Pais/ Ciudad / Pueblo
builder.Services.AddScoped<IServicioInterfazGenerica<Pais>, ServicioInerfazGenerica<Pais>>();
builder.Services.AddScoped<IServicioInterfazGenerica<Ciudad>, ServicioInerfazGenerica<Ciudad>>();
builder.Services.AddScoped<IServicioInterfazGenerica<Pueblo>, ServicioInerfazGenerica<Pueblo>>();

// Empleado
builder.Services.AddScoped<IServicioInterfazGenerica<Empleado>, ServicioInerfazGenerica<Empleado>>();

// Estados
builder.Services.AddScoped<TodosLosEstados>();

// Blazor
builder.Services.AddSyncfusionBlazor();
builder.Services.AddScoped<SfDialogService>();

await builder.Build().RunAsync();
