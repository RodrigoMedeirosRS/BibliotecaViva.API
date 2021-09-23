using BibliotecaViva.DAO;
using BibliotecaViva.DAL.Interfaces;

namespace BibliotecaViva.DAL 
{
    public abstract class BaseDAL
    {
        public bibliotecavivaContext DataContext { protected get; set; }

        public BaseDAL(bibliotecavivaContext dataContext)
        {
            DataContext = dataContext;
        }
    }
}