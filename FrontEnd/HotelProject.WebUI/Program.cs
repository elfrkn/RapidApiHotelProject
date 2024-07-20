using FluentValidation;
using FluentValidation.AspNetCore;
using HotelProject.DataAccessLayer.Concrete;
using HotelProject.EntityLayer.Concrete;
using HotelProject.WebUI.Dtos.GuestDto;
using HotelProject.WebUI.ValidationRules.GuestValidationRules;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<Context>();
builder.Services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<Context>();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddHttpClient();
builder.Services.AddTransient<IValidator<CreateGuestDto>, CreateGuestValidator>();
builder.Services.AddTransient<IValidator<UpdateGuestDto>, UpdateGuestValidator>();
builder.Services.AddControllersWithViews();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddMvc(config =>

//Proje seviyesinde Authorize
{
    var policy = new AuthorizationPolicyBuilder()
    .RequireAuthenticatedUser()
    .Build();
    config.Filters.Add(new AuthorizeFilter(policy));
});


builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(10); //10 dakika gecince cookie düşsün yeniden authentica olsun 
    options.LoginPath = "/Login/Index/"; // yönlendirme yapılacak sayfa 
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//Aranılan sayfa olmayınca çıkacak error page
app.UseStatusCodePagesWithReExecute("/ErrorPage/Error404", "?code={0}");
app.UseHttpsRedirection();


app.UseStaticFiles();
app.UseAuthentication();// Proje genelinde authentica

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
