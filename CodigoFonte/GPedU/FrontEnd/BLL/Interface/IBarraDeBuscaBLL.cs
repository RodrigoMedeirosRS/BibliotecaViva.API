using DTO;
using DTO.Dominio;

namespace BLL.Interface
{
    public interface IBarraDeBuscaBLL
    {
        void ObterPessoas(PessoaConsulta pessoaConsulta);
        void ObterRegistro(RegistroConsulta registroConsulta);
    }
}