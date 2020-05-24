namespace Carp
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
    using Microsoft.Extensions.DependencyInjection;
    using Carp.Parsers;
    using NetCore.AutoRegisterDi;
    using System.Reflection;

    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddTransient(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            //auto register services
            builder.Services.RegisterAssemblyPublicNonGenericClasses(Assembly.GetAssembly(typeof(Program)))
            .Where(c => c.Name.EndsWith("Service"))
            .AsPublicImplementedInterfaces();


            await builder.Build().RunAsync();
        }
    }
}
