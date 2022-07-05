using Microsoft.EntityFrameworkCore;
using Serilog;
using ShopifyInventory.Config;
using ShopifyInventory.Data;

var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    EnvironmentName = Environments.Production
});

// Add services to the container.
builder.Services.AddControllersWithViews();

//Add DbContext service
builder.Services.AddDatabase(builder.Environment);

//Apply Migrations
builder.Services.AddHostedService<InitMigration>();

//Add logging service
builder.Services.AddLogging();

builder.Host.UseSerilog((hostContext, services, configuration) => {
    configuration.WriteTo.Console();
});


//Dynamic port for Docker on Heroku
//var port = Environment.GetEnvironmentVariable("PORT");
//builder.WebHost.UseUrls($"http://+:{port}");


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
