using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Components.Authorization; // Add this line
using Microsoft.AspNetCore.Identity;
using DataLayer;
using FluentAssertions.Common;
using Microsoft.EntityFrameworkCore;
using BusinessLayer;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using BlazorServer.Authentication;

var builder = WebApplication.CreateBuilder(args);
//var connectionString = builder.Configuration.GetConnectionString("LibrarySystemDbContextConnection") ?? throw new InvalidOperationException("Connection string 'LibrarySystemDbContextConnection' not found.");

//builder.Services.AddDbContext<LibrarySystemDbContext>(options =>
//    options.UseSqlServer(connectionString));

//builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
//    .AddCookie();

//builder.Services.AddAuthorization(options =>
//{
//    options.AddPolicy("AdminPolicy", policy => policy.RequireRole("admin"));
//    options.AddPolicy("UserPolicy", policy => policy.RequireRole("user"));
//});

//builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true)
//    .AddRoles<IdentityRole>()
//    .AddEntityFrameworkStores<LibrarySystemDbContext>();



// Add services to the container.
builder.Services.AddAuthenticationCore();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped <ProtectedSessionStorage>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
builder.Services.AddSingleton<UserAccountService>();
builder.Services.AddScoped<BlazorServer.Pages.ErrorModel>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
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

app.UseAuthentication();
app.UseAuthorization();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
