using System;
using System.Threading.Tasks;
using System.Collections.Generic;
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
        private IPessoaBLL _BLL { get; set; }
        private IRequisicao _Requisicao { get; set; }
        
        public PessoaController(IPessoaBLL bll, IRequisicao requisicao)
        {
            _BLL = bll;
            _Requisicao= requisicao;
        }

        [HttpPost("Cadastrar")]
        public async Task<string> Cadastrar(PessoaDTO pessoa)
        {
            return _Requisicao.ExecutarRequisicao<PessoaDTO, string>(pessoa, _BLL.Cadastrar).Result;
        }

        [HttpPost("Consultar")]
        public async Task<List<PessoaDTO>> Consultar(PessoaConsulta pessoa)
        {
            return _Requisicao.ExecutarRequisicao<PessoaConsulta, List<PessoaDTO>>(pessoa, _BLL.Consultar).Result;
        }

        [HttpPost("ObterRelacoes")]
        public async Task<List<RegistroDTO>> Relacoes(string codRegistro)
        {
            return _Requisicao.ExecutarRequisicao<int, List<RegistroDTO>>(Convert.ToInt32(codRegistro), _BLL.ObterRelacoes).Result;
        }
    }
}