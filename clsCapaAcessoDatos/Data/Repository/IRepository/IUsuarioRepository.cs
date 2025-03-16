using clsModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clsCapaAcessoDatos.Data.Repository.IRepository
{
    public interface IUsuarioRepository : IRepository<AplicationUser>
    {
        public void BloquearUsuario(string IdUsuario);

        public void DesbloquearUsuario(string IdUsuario);
    }
}
