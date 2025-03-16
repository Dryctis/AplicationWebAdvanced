using clsModels;
using javax.swing.plaf;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AppWebAvanzada.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    //Poner aqui todos los modelos que se vayan creando
    public DbSet<clsCategorias> Categoria { get; set; }

    public DbSet<Articulo> Articulo { get; set; }

    public DbSet<Slider> Slider { get; set; }

    public DbSet<AplicationUser> AplicationUser { get; set; }

}
