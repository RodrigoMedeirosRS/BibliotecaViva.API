using BibliotecaViva.DTO;
using System.Collections.Generic;

namespace BibliotecaViva.DAL.Interfaces
{
    public interface ITipoExecucaoDAL
    {
        List<TipoExecucaoDTO> Listar();
    }
}