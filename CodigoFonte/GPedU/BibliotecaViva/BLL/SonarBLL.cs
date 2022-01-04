using System.Threading.Tasks;
using BibliotecaViva.DTO.Dominio;
using BibliotecaViva.BLL.Interfaces;
using BibliotecaViva.DAL.Interfaces;

namespace BibliotecaViva.BLL
{
    public class SonarBLL : BaseBLL, ISonarBLL
    {
        public ISonarDAL SonarDAL { get; set; } 

        public SonarBLL(ISonarDAL sonarDAL)
        {
            SonarDAL = sonarDAL;
        }
        public async Task<SonarRetorno> Consultar(SonarConsulta sonar)
        {
            return SonarDAL.Consultar(sonar);
        }
    }
}