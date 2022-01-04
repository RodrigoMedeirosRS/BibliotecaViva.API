using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using API.Interface;
using BibliotecaViva.DTO;
using BibliotecaViva.BLL.Interfaces;

namespace BibliotecaViva.Controllers
{
    [Route("Api/Tipo")]
    [ApiController]
    public class TipoController : Controller
    {
        private ITipoBLL _BLL { get; set; }
        private IRequisicao _Requisicao { get; set; }
        
        public TipoController(ITipoBLL bll, IRequisicao requisicao)
        {
            _BLL = bll;
            _Requisicao = requisicao;
        }

        [HttpPost("CadastrarIdioma")]
        public async Task<string> Cadastrar(IdiomaDTO idioma)
        {
            return _Requisicao.ExecutarRequisicao<IdiomaDTO, string>(idioma, _BLL.Cadastrar).Result;
        }


        [HttpPost("CadastrarTipo")]
        public async Task<string> Cadastrar(TipoDTO tipo)
        {
            return _Requisicao.ExecutarRequisicao<TipoDTO, string>(tipo, _BLL.Cadastrar).Result;
        }

        [HttpPost("CadastrarTipoRelacao")]
        public async Task<string> Cadastrar(TipoRelacaoDTO tipoRelacao)
        {
            return _Requisicao.ExecutarRequisicao<TipoRelacaoDTO, string>(tipoRelacao, _BLL.Cadastrar).Result;
        }

        [HttpPost("ConsultarIdiomas")]
        public async Task<List<IdiomaDTO>> ConsultarIdiomas(string entrada)
        {
            return _Requisicao.ExecutarRequisicao<List<IdiomaDTO>>(_BLL.ConsultarIdiomas).Result;
        }

        [HttpPost("ConsultarTipos")]
        public async Task<List<TipoDTO>> ConsultarTipos(string entrada)
        {
            return _Requisicao.ExecutarRequisicao<List<TipoDTO>>(_BLL.ConsultarTipos).Result;
        }

        [HttpPost("ConsultarTiposRelacao")]
        public async Task<List<TipoRelacaoDTO>> ConsultarTiposRelacao()
        {
            return _Requisicao.ExecutarRequisicao(_BLL.ConsultarTiposRelacao).Result;
        }
    }
}