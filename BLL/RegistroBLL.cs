using BibliotecaViva.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;
using BibliotecaViva.DTO.Dominio;
using BibliotecaViva.DAL.Interfaces;
using BibliotecaViva.BLL.Interfaces;

namespace BibliotecaViva.BLL
{
    public class RegistroBLL : BaseBLL, IRegistroBLL
    {
        private IRegistroDAL RegistroDAL { get; set; }
        private IReferenciaDAL ReferenciaDAL { get; set; }
        private IPessoaDAL PessoaDAL { get; set; }
        public RegistroBLL(IRegistroDAL registroDAL, IReferenciaDAL referenciaDAL, IPessoaDAL pessoaDAL)
        {
            RegistroDAL = registroDAL;
            ReferenciaDAL = referenciaDAL;
            PessoaDAL = pessoaDAL;
        }
        public async Task<string> Cadastrar(RegistroDTO registro) 
        {
            var codigo = RegistroDAL.Cadastrar(registro);
            return codigo + " Registrado(a) com Sucesso!";
        }
        public async Task<List<RegistroDTO>> Consultar(RegistroConsulta registro)
        {
            return RegistroDAL.Consultar(new RegistroDTO()
            {
                Nome = registro.Nome,
                Idioma = registro.Idioma
            });
        }
        public async Task<ReferenciaRetorno> ObterReferencias(int codRegistro)
        {
            var registro = new RegistroDTO()
            {
                Codigo = codRegistro
            };

            var registros = ReferenciaDAL.ObterReferenciaCompleta(registro, RegistroDAL);
            var pessoas = ReferenciaDAL.ObterPessoasReferenciadas(registro, PessoaDAL);

            return new ReferenciaRetorno()
            {
                Registros = registros ?? new List<RegistroDTO>(),
                Pessoas = pessoas ?? new List<PessoaDTO>()
            };
        }
    }
}
