using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using WebShop.Core.Data;
using WebShop.Core.Models.Identity;
using WebShop.App.ProgramOptionExtensions;

var builder = WebApplication.CreateBuilder(args);

if (builder.Environment.IsEnvironment("Testing"))
{
    builder.Configuration.AddUserSecrets<Program>(optional: true);
}

var connectionString = builder.Configuration
    .GetConnectionString("Default");

builder.Services
    .AddDbContext<ApplicationDbContext>(options => 
        options.UseSqlServer(connectionString));

builder.Services
    .AddIdentity<ApplicationUser, ApplicationRole>(options => options.AddOptions(builder.Configuration))
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddControllersWithViews();
builder.Services.AddDependencies();
builder.Services.AddAntiforgery();
builder.Services.AddRazorPages();
builder.Services.AddCors();
builder.AddConfigurations();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseCookiePolicy();
app.UseSession();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
    endpoints.AddEndPoints());

app.MapRazorPages();

await app.RunAsync();
