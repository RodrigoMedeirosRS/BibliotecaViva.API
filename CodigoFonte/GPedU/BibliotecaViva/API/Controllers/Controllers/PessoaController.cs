using System.Threading.Tasks.Dataflow;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using API.Interface;
using BibliotecaViva.DTO;
using BibliotecaViva.DTO.Dominio;
using BibliotecaViva.BLL.Interfaces;

namespace BibliotecaViva.Controllers
{
    [Route("Api/Pessoa")]
    [ApiController]
    public class PessoaController : Controller
    {
        private IPerssoaBLL _BLL { get; set; }
        private IRequisicao _Requisicao { get; set; }
        
        public PessoaController(IPerssoaBLL bll, IRequisicao requisicao)
        {
            _BLL = bll;
            _Requisicao= requisicao;
        }

        [HttpPost("Cadastrar")]
        public async Task<IActionResult> Cadastrar(PessoaDTO pessoa)
        {
            return Ok(_Requisicao.ExecutarRequisicao<PessoaDTO>(pessoa, _BLL.Cadastrar));
        }

        [HttpPost("Consultar")]
        public async Task<IActionResult> Consultar(PessoaConsulta pessoa)
        {
            return Ok(_Requisicao.ExecutarRequisicao<PessoaConsulta>(pessoa, _BLL.Consultar));
        }
    }
}