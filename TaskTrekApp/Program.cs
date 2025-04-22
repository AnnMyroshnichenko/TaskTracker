using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TaskTrekApp.Data;
using TaskTrekApp.Models;

DotNetEnv.Env.Load();
var connectionString = Environment.GetEnvironmentVariable("DEFAULT_CONNECTION_STRING");
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<AppDbContext>();

builder.Services.Configure<EmailSettings>(options =>
{
    options.Server = Environment.GetEnvironmentVariable("SMTP_SERVER"); ;
    options.Port = int.Parse(Environment.GetEnvironmentVariable("SMTP_PORT"));
    options.SenderName = Environment.GetEnvironmentVariable("SMTP_SENDERNAME");
    options.SenderEmail = Environment.GetEnvironmentVariable("SMTP_SENDEREMAIL");
    options.UserName = Environment.GetEnvironmentVariable("SMTP_USERNAME");
    options.Password = Environment.GetEnvironmentVariable("SMTP_PASSWORD");
});
builder.Services.AddTransient<IEmailService, EmailService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.MapRazorPages();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapStaticAssets();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
