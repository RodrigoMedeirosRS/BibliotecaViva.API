using System.Collections.Generic;
using BibliotecaViva.DTO;

namespace BibliotecaViva.DAL.Interfaces
{
    public interface IRegistroDAL
    {
        List<RegistroDTO> Consultar(RegistroDTO registroDTO);
        RegistroDTO Consultar(int codRegistro);
        void Cadastrar(RegistroDTO registroDTO);
    }
}
