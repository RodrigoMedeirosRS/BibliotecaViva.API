using BibliotecaViva.DTO.Dominio;

namespace BibliotecaViva.DAL.Interfaces
{
    public interface ISonarDAL
    {
        SonarRetorno Consultar(SonarConsulta registroDTO);
    }
}