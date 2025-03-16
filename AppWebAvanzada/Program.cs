using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using AppWebAvanzada.Data;
using clsCapaAcessoDatos.Data.Repository.IRepository;
using clsCapaAcessoDatos.Data.Repository;
using clsModels;
using clsCapaAcessoDatos.Data.Inicializador;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("ConexionSQL") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
//para  trabajar con roles adddefaultidentity se cambia por addidentity y se agrega, identityrole
builder.Services.AddIdentity<AplicationUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultUI();
builder.Services.AddControllersWithViews();

//Agregar contenedor de trabajo al contenedor IoC de inyeccion de dependencias 
builder.Services.AddScoped<IContenedorTrabajo, ContenedorTrabajo>();

//Siembra de datos-paso1
builder.Services.AddScoped<IInicializadorBD, InicializadorBD>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

//Metodo que ejecuta la siembra de datos-paso2

siembraDatos();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{area=Cliente}/{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();

//Funcionalidad metodo Siembra de datos
void siembraDatos()
{
    
    using (var scope = app.Services.CreateScope())
    {
        var inicializador = scope.ServiceProvider.GetRequiredService<IInicializadorBD>();
        inicializador.Inicializar();
    }
}
