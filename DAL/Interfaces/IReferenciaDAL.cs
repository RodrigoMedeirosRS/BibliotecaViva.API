using BibliotecaViva.DTO;
using System.Collections.Generic;

namespace BibliotecaViva.DAL.Interfaces
{
    public interface IReferenciaDAL
    {
        void VincularReferencia(RegistroDTO registroDTO);
        List<RelacaoDTO> ObterReferencia(int codRegistro);
        List<RegistroDTO> ObterReferenciaCompleta(RegistroDTO registroDTO, IRegistroDAL registroDAL);
        List<PessoaDTO> ObterPessoasReferenciadas(RegistroDTO registroDTO, IPessoaDAL pessoaDAL);
    }
}