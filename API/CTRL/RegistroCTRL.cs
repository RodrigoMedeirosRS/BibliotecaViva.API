using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

using API.Interface;
using BibliotecaViva.DTO;
using BibliotecaViva.DTO.Dominio;
using BibliotecaViva.BLL.Interfaces;

namespace BibliotecaViva.CTRL
{
    [Route("BibliotecaViva/Registro")]
    [ApiController]
    public class RegistroController : Controller
    {
        private IRegistroBLL _BLL { get; set; }
        private IRequisicao _Requisicao { get; set; }
        
        public RegistroController(IRegistroBLL bll, IRequisicao requisicao)
        {
            _BLL = bll;
            _Requisicao = requisicao;
        }

        [HttpPost("Cadastrar")]
        public async Task<string> Cadastrar(RegistroDTO registro)
        {
            return _Requisicao.ExecutarRequisicao<RegistroDTO, string>(registro, _BLL.Cadastrar).Result;
        }

        [HttpPost("Consultar")]
        public async Task<List<RegistroDTO>> Consultar(RegistroConsulta registro)
        {
            return _Requisicao.ExecutarRequisicao<RegistroConsulta, List<RegistroDTO>>(registro, _BLL.Consultar).Result;
        }

        [HttpPost("ObterReferencias")]
        public async Task<ReferenciaRetorno> ObterReferencias(RelacaoConsulta Relacao)
        {
            return _Requisicao.ExecutarRequisicao<int, ReferenciaRetorno>(Relacao.CodRegistro, _BLL.ObterReferencias).Result;
        }
    }
}
