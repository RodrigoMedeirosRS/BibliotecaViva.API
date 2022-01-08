using System.Threading.Tasks;
using System.Collections.Generic;
using BibliotecaViva.DTO;
using BibliotecaViva.DTO.Dominio;

namespace BibliotecaViva.BLL.Interfaces
{
    public interface IPessoaBLL
    {
        Task<string> Cadastrar(PessoaDTO pessoa);
        Task<List<PessoaDTO>> Consultar(PessoaConsulta pessoa);
        Task<List<RegistroDTO>> ObterRelacoes(int codPessoa);
    }
}
