using System.Threading.Tasks;
using BibliotecaViva.DTO.Dominio;

namespace BibliotecaViva.BLL.Interfaces
{
    public interface ISonarBLL
    {
        Task<SonarRetorno> Consultar(SonarConsulta sonar);
    }
}