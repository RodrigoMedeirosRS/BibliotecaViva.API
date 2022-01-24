using System.Threading.Tasks;
using System.Collections.Generic;

using BibliotecaViva.DAL;
using BibliotecaViva.DTO;
using BibliotecaViva.DAL.Interfaces;
using BibliotecaViva.BLL.Interfaces;

namespace BibliotecaViva.BLL
{
    public class RastrosBLL : IRastrosBLL
    {
        private IRastrosDAL RastrosDAL { get; set; }
        public RastrosBLL(IRastrosDAL rastrosDAL)
        {
            RastrosDAL = rastrosDAL;
        }
        public async Task<List<LocalizacaoGeograficaDTO>> Consultar()
        {
            return RastrosDAL.Consultar();
        }
    }
}