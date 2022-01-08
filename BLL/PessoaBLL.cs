using System.Threading.Tasks;
using System.Collections.Generic;
using BibliotecaViva.DTO;
using BibliotecaViva.DTO.Dominio;
using BibliotecaViva.DAL.Interfaces;
using BibliotecaViva.BLL.Interfaces;

namespace BibliotecaViva.BLL
{
    public class PessoaBLL : BaseBLL, IPessoaBLL
    {
        private IPessoaDAL PessoaDAL { get; set; }
        private IPessoaRegistroDAL PessoaRegistroDAL { get; set; }
        public PessoaBLL(IPessoaDAL referenciaDAL, IPessoaRegistroDAL pessoaRegistroDAL)
        {
            PessoaDAL = referenciaDAL;
            PessoaRegistroDAL = pessoaRegistroDAL;
        }

        public async Task<string> Cadastrar(PessoaDTO pessoa)
        {
            PessoaDAL.Cadastrar(pessoa);
            return pessoa.Nome + " " + pessoa.Sobrenome + " Registrado(a) com Sucesso!";
        }

        public async Task<List<PessoaDTO>> Consultar(PessoaConsulta pessoa)
        {
            return PessoaDAL.Consultar(new PessoaDTO()
            {
                Nome = pessoa.Nome,
                Sobrenome = pessoa.Sobrenome
            });
        }

        public async Task<List<RegistroDTO>> ObterRelacoes(int codPessoa)
        {
            return PessoaRegistroDAL.ObterRelacaoCompleta(new PessoaDTO()
            {
                Codigo = codPessoa
            });
        }
    }
}