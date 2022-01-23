using System;
using System.Threading.Tasks;
using System.Globalization;

using BibliotecaViva.DTO;
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
            return SonarDAL.Consultar(ParsearCoordenadas(sonar));
        }
        private SonarDTO ParsearCoordenadas(SonarConsulta sonar)
        {
            
            return new SonarDTO()
            {
                Latitude = double.Parse(sonar.Latitude, System.Globalization.NumberStyles.Any, CultureInfo.GetCultureInfo("en-US")),
                Longitude = double.Parse(sonar.Longitude, System.Globalization.NumberStyles.Any, CultureInfo.GetCultureInfo("en-US")),
                Alcance = double.Parse(sonar.Alcance, System.Globalization.NumberStyles.Any, CultureInfo.GetCultureInfo("en-US"))
            };
        }
    }
}