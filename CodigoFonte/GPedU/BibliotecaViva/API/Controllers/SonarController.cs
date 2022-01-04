using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

using API.Interface;
using BibliotecaViva.DTO;
using BibliotecaViva.DTO.Dominio;
using BibliotecaViva.BLL.Interfaces;

namespace BibliotecaViva.Controllers
{
    [Route("Api/Sonar")]
    [ApiController]
    public class SonarController : Controller
    {
        private ISonarBLL _BLL { get; set; }
        private IRequisicao _Requisicao { get; set; }
        
        public SonarController(ISonarBLL bll, IRequisicao requisicao)
        {
            _BLL = bll;
            _Requisicao = requisicao;
        }

        [HttpPost("Consultar")]
        public async Task<SonarRetorno> Consultar(SonarConsulta sonar)
        {
            return _Requisicao.ExecutarRequisicao<SonarConsulta, SonarRetorno>(sonar, _BLL.Consultar).Result;
        }
    }
}