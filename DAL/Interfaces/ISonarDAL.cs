using BibliotecaViva.DTO;
using BibliotecaViva.DTO.Dominio;

namespace BibliotecaViva.DAL.Interfaces
{
    public interface ISonarDAL
    {
        SonarRetorno Consultar(SonarDTO registroDTO);
    }
}