﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using BibliotecaViva.DTO;
using BibliotecaViva.BLL.Interfaces;

namespace BibliotecaViva.Controllers
{
    [Route("Api/Documento/Imagem")]
    [ApiController]
    public class ImagemController : Controller
    {
        private IDocumentoBLL _BLL;
        public ImagemController(IDocumentoBLL bll)
        {
            _BLL = bll;
        }

        [HttpPost("Cadastrar")]
        public async Task<IActionResult> Cadastrar(ImagemDTO documento)
        {
            try
            {
                return Ok(await Task.Run(async () => await _BLL.Cadastrar(documento)));
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPost("Consultar")]
        public async Task<IActionResult> Consultar(ImagemDTO documento)
        {
            try
            {
                return Ok(await Task.Run(async () => await _BLL.Consultar(documento)));
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPost("Mencionar")]
        public async Task<IActionResult> Mencionar(ImagemDTO documento)
        {
            try
            {
                return Ok(await Task.Run(async () => await _BLL.Mencionar(documento)));
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
