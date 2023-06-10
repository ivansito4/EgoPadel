using EgoPadel.Datos;
using EgoPadel.Infrastructura;
using EgoPadel.Models;
using EgoPadel.Servicios;
using EgoPadel.Utilidades;
using EgoPadel.Utilidades.BrainTree;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
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

//Lo del braintree
builder.Services.Configure<BrainTreeSetings>(builder.Configuration.GetSection("BrainTree"));
builder.Services.AddSingleton<IBrainTreeGate, BrainTreeGate>();


builder.Services.AddIdentityCore<UsuarioApp>().AddRoles<IdentityRole>()
    .AddClaimsPrincipalFactory<UserClaimsPrincipalFactory<UsuarioApp, IdentityRole>>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders().AddDefaultUI();

builder.Services.AddTransient<IUnitOfWork,UnitOfWork>();

//Para enviar correos
builder.Services.AddTransient<IEmailSender, EmailSender>();


//Login con facebook
builder.Services.AddAuthentication().AddFacebook(options =>
{
    options.AppId = "240393538631557";
    options.AppSecret = "e5259dad14f48ab13c7ad447cf501522";
});

//Para tener sesiones
builder.Services.AddHttpContextAccessor();
builder.Services.AddSession(Options =>
{
    Options.IdleTimeout = TimeSpan.FromMinutes(10);
    Options.Cookie.HttpOnly = true;
    Options.Cookie.IsEssential = true;
});


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

//Pipeline para usar sesiones
app.UseSession();  

//La libreria de usuarios necesita usar el modelo Razor
app.MapRazorPages(); 

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
