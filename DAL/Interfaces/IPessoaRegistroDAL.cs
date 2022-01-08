using System.Collections.Generic;
using BibliotecaViva.DTO;

namespace BibliotecaViva.DAL.Interfaces
{
    public interface IPessoaRegistroDAL
    {
        void VincularReferencia(PessoaDTO pessoaDTO);
        List<PessoaRegistroDTO> ObterRelacao(int codPessoa);
        List<RegistroDTO> ObterRelacaoCompleta(PessoaDTO pessoaDTO);
    }
}