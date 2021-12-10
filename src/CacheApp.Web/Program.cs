using CacheApp.Web.Contracts;
using CacheApp.Web.Data.Caching;
using CacheApp.Web.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var servidor = builder.Configuration["MySQLConnection:server"];
var database = builder.Configuration["MySQLConnection:database"];
var user = builder.Configuration["MySQLConnection:user"];
var password = builder.Configuration["MySQLConnection:password"];

builder.Services.AddDbContext<AgendaDbContext>(
            dbContextOptions => dbContextOptions
                .UseMySql($"server={servidor};database={database};user={user};password={password}", new MySqlServerVersion(new Version(8, 0, 27)))
                .LogTo(Console.WriteLine, LogLevel.Information)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors()
        );

/*  Registrando Decorator de Cache 
builder.Services.AddScoped<ContatoRepository>();
builder.Services.AddScoped<IContatoRepository, ContatoCacheDecorator<ContatoRepository>>();
*/

builder.Services.AddScoped<IContatoRepository, ContatoRepository>();
builder.Services.Decorate<IContatoRepository, ContatoCacheDecoratorScrutor>();

var redisConfig = builder.Configuration["RedisConnection"];

builder.Services.AddDistributedRedisCache(opt =>
{
    opt.Configuration = builder.Configuration["RedisConnection"];
    opt.InstanceName = "Agenda";
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
