using System.Collections.Generic;
using BibliotecaViva.DTO;

namespace BibliotecaViva.DAL.Interfaces
{
    public interface IIdiomaDAL
    {
        void Cadastrar(IdiomaDTO idiomaDTO);
        IdiomaDTO Consultar(IdiomaDTO idiomaDTO);
        List<IdiomaDTO> Listar();
    }
}