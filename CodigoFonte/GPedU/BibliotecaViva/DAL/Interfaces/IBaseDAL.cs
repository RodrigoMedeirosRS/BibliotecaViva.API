using BibliotecaViva.DAO;

namespace BibliotecaViva.DAL.Interfaces
{
    public interface IBaseDAL
    {
        bibliotecavivaContext DataContext { set; }
    }
}