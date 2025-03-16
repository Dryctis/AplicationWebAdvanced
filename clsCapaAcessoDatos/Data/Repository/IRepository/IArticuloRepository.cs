using clsModels;
using System.Collections.Generic;

namespace clsCapaAcessoDatos.Data.Repository.IRepository
{
    public interface IArticuloRepository : IRepository<Articulo>
    {
        void Update(Articulo articulo);

        //Metodo para el buscador
        IQueryable<Articulo> AsQueryable();
    }
}
