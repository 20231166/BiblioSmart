using BiblioSmart.Core.Interfaces;
using BiblioSmart.Web.Services;

var builder = WebApplication.CreateBuilder(args);

// Servicios en memoria (sin base de datos)
builder.Services.AddSingleton<ILibroRepository, InMemoryLibroRepository>();
builder.Services.AddSingleton<IMiembroRepository, InMemoryMiembroRepository>();
builder.Services.AddSingleton<IPrestamoRepository, InMemoryPrestamoRepository>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
