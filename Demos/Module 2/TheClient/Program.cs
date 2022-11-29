
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Http;
using Microsoft.Extensions.DependencyInjection;

namespace TheClient;
class Program
{
    static IHost host;

    static void Main(string[] args)
    {
         var bld = Host.CreateDefaultBuilder(args);
         
         bld.ConfigureServices(svcs=>{
            svcs.AddHttpClient("local", conf=>{
                conf.BaseAddress = new Uri("https://localhost:8001/");       
            }).SetHandlerLifetime(TimeSpan.FromMinutes(5));
        });

        host = bld.Build();

        //CallDirectAsync();
        //ManyCalls();
        ViaFactoryAsync();
        host.Run();
        Console.ReadLine();
    }

    
    private static async Task ViaFactoryAsync()
    {
        var httpfact = host.Services.GetRequiredService<IHttpClientFactory>();
        var client = httpfact.CreateClient("local");
        var result = client.GetAsync("home/sub/67").Result;
        if (result.IsSuccessStatusCode)
        {
             var data = await result.Content.ReadAsStringAsync();
            System.Console.WriteLine(   data);
        }
    }

    static HttpClient client = new HttpClient();
    private static void ManyCalls()
    {
        client.BaseAddress = new Uri("https://localhost:8001/");
        for(int i = 0; i < 2000; i++)
        {
            CallDirectAsync();
        }
    }

    private static async Task CallDirectAsync()
    {
        client.BaseAddress = new Uri("https://localhost:8001/");
        var result = client.GetAsync("home/sub/67").Result;
        if (result.IsSuccessStatusCode)
        {
            foreach(var head in result.Headers)
            {
                System.Console.WriteLine($"{head.Key}: {head.Value}");
            }
            System.Console.WriteLine("De body ========================");
            foreach(var head in result.Content.Headers)
            {
                System.Console.WriteLine($"{head.Key}: {head.Value.First()}");
            }
            var data = await result.Content.ReadAsStringAsync();
            System.Console.WriteLine(   data);
        }
        //client.Dispose();
    }
}
