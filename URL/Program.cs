using Microsoft.EntityFrameworkCore;
using URL.Model;
using URL.service;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularFrontend", builder =>
    {
        builder.WithOrigins("https://localhost:44404")
        .AllowAnyMethod()
        .AllowAnyHeader();
    });
});

// Add services to the container.

builder.Services.AddControllersWithViews();
var defaultConnection = builder.Configuration.GetConnectionString("DefaultConnection");


builder.Services.AddControllers();
var settings = new Appsetting();
//settings.connectionStrings = new ConnectionStrings { DefaultConnection = defaultConnection };
builder.Services.AddDbContext<Urlcontext>(Option =>
{
    Option.UseSqlServer(defaultConnection);
});
var app = builder.Build();
app.UseCors("AllowAngularFrontend");
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html"); ;

app.Run();
