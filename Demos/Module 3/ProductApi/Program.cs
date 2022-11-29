using Microsoft.EntityFrameworkCore;
using ProductApi.ConfigClasses;
using ProductApi.Middleware;
using ProductReviews.DAL.EntityFramework.Database;
using ProductReviews.Interfaces;
using ProductReviews.Repositories.EntityFramework;

//var constr = @"Server=.\sqlexpress;Database=Mod1DB;Trusted_Connection=True;";
var builder = WebApplication.CreateBuilder(args);
//var constr = builder.Configuration.GetSection("MyConfig:Constr").Value;
//var confObj = builder.Configuration.GetSection("MyConfig").Get<MyConfig>();

//var confObj = new MyConfig();
//builder.Configuration.GetSection("MyConfig").Bind(confObj);

var constr = Environment.GetEnvironmentVariable("MY_CONST");
System.Console.WriteLine(constr);
// Add services to the container.
builder.Services.AddDbContext<ProductReviewsContext>(opts=>{
    opts.UseSqlServer(constr);
});
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.Use(async (ctx, next)=>{
//     System.Console.WriteLine("Heenweg");
//     await next(ctx);
//     System.Console.WriteLine("Terugweg");

// });

//app.UseMiddleware<HeenEnweerMiddleware>();

app.UseHeenEnWeer();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
