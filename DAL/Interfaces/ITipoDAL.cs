using BibliotecaViva.DTO;
using System.Collections.Generic;

namespace BibliotecaViva.DAL.Interfaces
{
    public interface ITipoDAL
    {
        void Cadastrar(TipoDTO tipoDTO);
        TipoDTO Consultar(TipoDTO tipoDTO);
        List<TipoDTO> Listar();
    }
}