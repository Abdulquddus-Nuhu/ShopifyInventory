using Microsoft.EntityFrameworkCore;
using Serilog;
using ShopifyInventory.Config;
using ShopifyInventory.Data;
using Microsoft.AspNetCore.Identity;
using ShopifyInventory.Services;
using Microsoft.AspNetCore.Identity.UI.Services;

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

//Add Authentication
builder.Services.AddAuth();

//Add logging service
builder.Host.UseSerilog((hostContext, services, configuration) => {
    configuration.WriteTo.Console();
});

//Register Email Service
builder.Services.Configure<SmtpOptions>(builder.Configuration.GetSection("Smtp"));
builder.Services.AddTransient<IEmailSender, SmtpEmailSender>();

builder.Services.AddHttpContextAccessor();


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
app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
