﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using API.Interface;
using BibliotecaViva.DTO;
using BibliotecaViva.DTO.Dominio;
using BibliotecaViva.BLL.Interfaces;

namespace BibliotecaViva.Controllers
{
    [Route("Api/Documento/Imagem")]
    [ApiController]
    public class ImagemController : Controller
    {
        private IDocumentoBLL _BLL { get; set; }
        private IRequisicao _Requisicao { get; set; }
        public ImagemController(IDocumentoBLL bll, IRequisicao requisicao)
        {
            _BLL = bll;
            _Requisicao = requisicao;
        }

        [HttpPost("Cadastrar")]
        public async Task<IActionResult> Cadastrar(ImagemDTO documento)
        {
            return Ok(_Requisicao.ExecutarRequisicao<ImagemDTO>(documento, _BLL.Cadastrar));
        }

        [HttpPost("Consultar")]
        public async Task<IActionResult> Consultar(ImagemConsulta documento)
        {
            return Ok(_Requisicao.ExecutarRequisicao<ImagemConsulta>(documento, _BLL.Consultar));
        }
    }
}
