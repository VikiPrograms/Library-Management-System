using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Components.Authorization; // Add this line
using DataLayer;
using Microsoft.EntityFrameworkCore;
using BusinessLayer;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using BlazorServer.Authentication;
using ServiceLayer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
    builder.Services.AddAuthenticationCore();
    builder.Services.AddRazorPages();
    builder.Services.AddServerSideBlazor();
    builder.Services.AddScoped<UserContext, UserContext>();
    builder.Services.AddScoped<_UserManager, _UserManager>();
    builder.Services.AddScoped<UserManager<ApplicationUser>, UserManager<ApplicationUser>>();
    builder.Services.AddScoped <ProtectedSessionStorage>();
    builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
    builder.Services.AddScoped<ReadingCardContext, ReadingCardContext>();
    builder.Services.AddScoped<BlazorServer.Pages.ErrorModel>();
    builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<LibrarySystemDbContext>().AddDefaultTokenProviders();
    builder.Services.AddScoped<LibrarySystemDbContext, LibrarySystemDbContext>();


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
