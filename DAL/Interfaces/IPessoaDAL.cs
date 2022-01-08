using System.Collections.Generic;
using BibliotecaViva.DTO;

namespace BibliotecaViva.DAL.Interfaces
{
    public interface IPessoaDAL
    {
        void Cadastrar(PessoaDTO pessoaDTO);
        List<PessoaDTO> Consultar(PessoaDTO pessoaDTO);        
    }
}
