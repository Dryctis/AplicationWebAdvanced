using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using clsModels;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace clsCapaAcessoDatos.Data.Repository.IRepository
{
    public interface ICategoriaRepository : IRepository<clsCategorias>
    {
        void update(clsCategorias categoria);

        IEnumerable<SelectListItem> GetListCategorias();
    }
}
