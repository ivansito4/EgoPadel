using EgoPadel.Datos;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Configura la bbdd y  se conecta a la defaultConnection que esta en appsettings.json
builder.Services.AddDbContext<ApplicationDbContext>(options => 
                              options.UseSqlServer(
                              builder.Configuration.GetConnectionString("DefaultConnection")));

//Agrega el servicio para usar la libreria de Scaffold (Cosas de los Usuarios) y roles
builder.Services.AddIdentity<IdentityUser,IdentityRole>()
                              .AddDefaultTokenProviders().AddDefaultUI()
                              .AddEntityFrameworkStores<ApplicationDbContext>();


builder.Services.AddControllersWithViews();

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

//Primero se autentica y luego se autoriza, si no da error
app.UseAuthentication();
app.UseAuthorization();

//app.UseSession();   MIRAR ?????????????????????????????????????????????????????

//La libreria de usuarios necesita usar el modelo Razor
app.MapRazorPages(); 

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
