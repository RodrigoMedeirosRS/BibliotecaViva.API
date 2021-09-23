using MoreLinq;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using BibliotecaViva.DAO;
using BibliotecaViva.DTO;
using BibliotecaViva.DAL.Utils;
using BibliotecaViva.DAL.Interfaces;


namespace BibliotecaViva.DAL
{
    public class PessoaRegistroDAL : BaseDAL, IPessoaRegistroDAL
    {
        ITipoRelacaoDAL TipoRelacaoDAL { get; set; }
        IRegistroDAL RegistroDAL { get; set; }
        public PessoaRegistroDAL(bibliotecavivaContext dataContext, ITipoRelacaoDAL tipoRelacaoDAL, IRegistroDAL registroDAL) : base(dataContext)
        {
            TipoRelacaoDAL = tipoRelacaoDAL;
            RegistroDAL = registroDAL;
        }

        public void VincularReferencia(PessoaDTO pessoaDTO)
        {
            var relacoes = ListarRelacoes((int)pessoaDTO.Codigo);

            foreach (var ralacao in relacoes)
                DataContext.Pessoaregistros.Remove(ralacao);

            foreach (var relacao in pessoaDTO.Relacoes)
                DataContext.Add(new Pessoaregistro()
                {
                    Pessoa = (int)relacao.Pessoa,
                    Registro = (int)relacao.Registro,
                    Tiporelacao = (int)TipoRelacaoDAL.Consultar(new TipoRelacaoDTO()
                    {
                        Nome = relacao.TipoRelacao
                    }).Codigo
                });
        }
        public List<PessoaRegistroDTO> ObterRelacao(int codPessoa)
        {
            return (from relacao in DataContext.Pessoaregistros
                where 
                    relacao.Pessoa == codPessoa
                select 
                    new PessoaRegistroDTO()
                    {
                        Codigo = relacao.Codigo,
                        Registro = (int)relacao.Registro,
                        Pessoa = (int)relacao.Pessoa,
                        TipoRelacao = TipoRelacaoDAL.Consultar(new TipoRelacaoDTO()
                        {
                            Codigo = relacao.Tiporelacao
                        }).Nome
                    }).AsNoTracking().ToList();
        }
        public List<RegistroDTO> ObterRelacaoCompleta(PessoaDTO pessoaDTO)
        {
            var relacoes = ListarRelacoes((int)pessoaDTO.Codigo);
            
            var registros = new List<RegistroDTO>();

            if (relacoes == null)
                return registros;

            foreach(var relacao in relacoes)
                registros.Add(RegistroDAL.Consultar((int)relacao.Registro));

            return registros;
        }
        private IQueryable<Pessoaregistro> ListarRelacoes(int codPessoa)
        {
            return DataContext.Pessoaregistros.Where(relacao => relacao.Pessoa == codPessoa);
        }
    }
}