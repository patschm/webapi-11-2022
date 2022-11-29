using LowLevel.Services;

class Program
{
    static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        ConfigureServices(builder.Services);
        var app = builder.Build();
        Configure(app);
        app.Run();
    }
    private static void ConfigureServices(IServiceCollection services)
    {
        services.AddScoped<Counter, Counter>();
        services.AddSwaggerGen();
        services.AddControllers(); // WebApi
        //services.AddControllersWithViews(); // MVC
        
    }
    private static void Configure(WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        //app.MapGet("/test/{id:int}", (int id) => $"Hello World! {id}");
        app.MapControllers();
        //app.MapControllerRoute("route1", "{controller}/{action}/{id:int}", defaults: new {controller="home", action="index", id=50});
    }


}


