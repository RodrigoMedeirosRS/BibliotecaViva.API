using System.Threading.Tasks;
using System.Collections.Generic;
using BibliotecaViva.DTO;

namespace BibliotecaViva.BLL.Interfaces
{
    public interface IRastrosBLL
    {
        Task<List<LocalizacaoGeograficaDTO>> Consultar();
    }
}