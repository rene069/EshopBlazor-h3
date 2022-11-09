using Blazored.Toast;
using EshopBlazor;
using EshopBlazor.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace EshopBlazor
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");
            builder.Services.AddBlazoredToast();
            builder.Services.AddScoped<IProduktServices,ProduktAPIServices>();
            //builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddHttpClient<IProduktServices, ProduktAPIServices>(config => config.BaseAddress = AppConfig.Todo_BaseAddress);

            await builder.Build().RunAsync();
        }
    }
}