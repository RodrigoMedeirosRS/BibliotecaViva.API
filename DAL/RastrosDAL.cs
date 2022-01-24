using MoreLinq;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using BibliotecaViva.DAO;
using BibliotecaViva.DTO;
using BibliotecaViva.DAL.Utils;
using BibliotecaViva.DAL.Interfaces;


namespace BibliotecaViva.DAL
{
    public class RastrosDAL : BaseDAL, IRastrosDAL
    {
        public RastrosDAL(bibliotecavivaContext dataContext) : base(dataContext)
        {
        }
        public List<LocalizacaoGeograficaDTO> Consultar()
        {
            return new List<LocalizacaoGeograficaDTO>();
        }
    }
}