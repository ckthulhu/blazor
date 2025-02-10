using Azure.Identity;
using Microsoft.EntityFrameworkCore;
using Radzen;
using RadzenApp.Components;
using RadzenApp.Data;

var builder = WebApplication.CreateBuilder(args);

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json")
    .Build();


// Entity Framework
builder.Services.AddDbContextFactory<ScanContext>(option =>
    option.UseSqlServer(configuration["ConnectionStrings:Test"]));
//builder.Services.AddDbContextFactory<ALTCADContext>(option =>
//    option.UseSqlServer(configuration["ConnectionStrings:ALTCAD"]));


// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddRadzenComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
