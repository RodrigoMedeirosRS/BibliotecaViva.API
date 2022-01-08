using BibliotecaViva.DTO;
using System.Collections.Generic;

namespace BibliotecaViva.DAL.Interfaces
{
    public interface IReferenciaDAL
    {
        void VincularReferencia(RegistroDTO registroDTO);
        List<ReferenciaDTO> ObterReferencia(int codRegistro);
        List<RegistroDTO> ObterReferenciaCompleta(RegistroDTO registroDTO, IRegistroDAL registroDAL);
    }
}