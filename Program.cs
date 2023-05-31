
using DuendeIdentityServer.Data;
using DuendeIdentityServer.IDBInitializer;
using DuendeIdentityServer.Models;
using DuendeIdentityServer.StaticDetails;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add DBContext Service
builder.Services.AddDbContext<ApplicationDBContext>(options=>
                options.UseSqlServer(builder.Configuration.GetConnectionString("CMSConnectionString")));

// Add Identity Services
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDBContext>().AddDefaultTokenProviders();

//Add Service for DB Initializer
builder.Services.AddScoped<IDBInitializer, DBInitializer>();

builder.Services.AddRazorPages();

//Add Identity Server Services
builder.Services.AddIdentityServer(options =>
{
    options.Events.RaiseErrorEvents = true;
    options.Events.RaiseInformationEvents = true;
    options.Events.RaiseFailureEvents = true;
    options.Events.RaiseSuccessEvents = true;
    options.EmitStaticAudienceClaim = true;
}).AddInMemoryIdentityResources(SD.GetIdentityResources())
.AddInMemoryApiScopes(SD.GetApiScopes()).AddInMemoryClients(SD.GetClients())
.AddAspNetIdentity<ApplicationUser>()
.AddDeveloperSigningCredential();


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

// Call the SeedDatabase method to initialize the data
SeedDatabase();

app.UseRouting();

app.UseAuthorization();
app.UseIdentityServer();
app.MapRazorPages().RequireAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();


// Create a method to seed the database
void SeedDatabase()
{
    using(var scope = app.Services.CreateScope())
    {
        var dbInitializer = scope.ServiceProvider.GetRequiredService<IDBInitializer>();
        dbInitializer.Initialize();
    }
}