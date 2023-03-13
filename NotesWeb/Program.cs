using DataAccess.DBContexts;
using Microsoft.AspNetCore.Identity;
using DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using DataAccess.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connstring = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<NotesDbContext>(options =>
    options.UseSqlServer(connstring, b => b.MigrationsAssembly("NotesWeb")));

builder.Services.AddScoped<INotesRepository, NotesRepository>();
builder.Services.AddScoped<IAuditLogRepository, AuditLogRepository>();

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<NotesDbContext>();
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
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
