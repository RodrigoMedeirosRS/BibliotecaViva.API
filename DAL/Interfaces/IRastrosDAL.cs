using BibliotecaViva.DTO;
using System.Collections.Generic;

namespace BibliotecaViva.DAL.Interfaces
{
    public interface IRastrosDAL
    {
        List<LocalizacaoGeograficaDTO> Consultar();
    }
}