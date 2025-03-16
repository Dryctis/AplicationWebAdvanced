using AppWebAvanzada.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using clsModels;
using Microsoft.EntityFrameworkCore;
using clsUtilidades;

namespace clsCapaAcessoDatos.Data.Inicializador
{
    public class InicializadorBD : IInicializadorBD
    {
        private readonly ApplicationDbContext _bd;
        private readonly UserManager<AplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        //creamos el constructor

        public InicializadorBD(ApplicationDbContext bd, UserManager<AplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _bd = bd;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public void Inicializar()
        {
            try
            {
                if (_bd.Database.GetPendingMigrations().Count() > 0)
                {
                    _bd.Database.Migrate();
                }
            }
            catch (Exception)
            {


            }
            if (_bd.Roles.Any(ro => ro.Name == CNT.Administrador)) return;

            //creacion de roles
            _roleManager.CreateAsync(new IdentityRole(CNT.Administrador)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(CNT.Registrado)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(CNT.Cliente)).GetAwaiter().GetResult();

            //creacion de usuario inicial
            _userManager.CreateAsync(new AplicationUser
            {
                UserName = "test1@gmail.com",
                Email = "Test1@gmail.com",
                EmailConfirmed = true,
                nombre = "Test1",
                direccion = "Direccion de ejemplo",
                ciudad = "Ciudad de ejemplo",
                pais = "Pais de ejemplo"
            }, "Admin123#").GetAwaiter().GetResult();

            AplicationUser usuario = _bd.AplicationUser.Where(u => u.Email == "Test1@gmail.com").FirstOrDefault();
            _userManager.AddToRoleAsync(usuario, CNT.Administrador).GetAwaiter().GetResult();
        
        }
    }
}