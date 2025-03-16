using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clsModels
{
    public class ListaPaginada<T> : List<T>
    {
        public int PagedIndex { get; private set; }

        public int TotalPages { get; private set; }

        public string SearchString { get; private set; }

        public ListaPaginada(List<T> items, int count, int pageIndex, int pageSize, string searchString)
        {
            PagedIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            SearchString = searchString;

            AddRange(items);
        }

        public bool HasPreviousPage => (PagedIndex > 1);

        public bool HasNextPage => (PagedIndex < TotalPages);



    }
}
