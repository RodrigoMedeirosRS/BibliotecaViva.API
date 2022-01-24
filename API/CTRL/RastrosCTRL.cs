using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

using API.Interface;
using BibliotecaViva.DTO;
using BibliotecaViva.DTO.Dominio;
using BibliotecaViva.BLL.Interfaces;


namespace API.CTRL
{
    [Route("BibliotecaViva/Rastros")]
    [ApiController]
    public class RastrosController : Controller
    {
        private IRastrosBLL _BLL { get; set; }
        private IRequisicao _Requisicao { get; set; }
        
        public RastrosController(IRastrosBLL bll, IRequisicao requisicao)
        {
            _BLL = bll;
            _Requisicao = requisicao;
        }

        [HttpPost("Consultar")]
        public async Task<List<LocalizacaoGeograficaDTO>> Consultar(string entrada)
        {
            return _Requisicao.ExecutarRequisicao<List<LocalizacaoGeograficaDTO>>(_BLL.Consultar).Result;
        }
    }
}