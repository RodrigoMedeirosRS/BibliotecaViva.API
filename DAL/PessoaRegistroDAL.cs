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
        private ITipoRelacaoDAL TipoRelacaoDAL { get; set; }
        private IRegistroDAL RegistroDAL { get; set; }
        private IReferenciaDAL ReferenciaDAL { get; set; }
        public PessoaRegistroDAL(bibliotecavivaContext dataContext, ITipoRelacaoDAL tipoRelacaoDAL, IRegistroDAL registroDAL, IReferenciaDAL referenciaDAL) : base(dataContext)
        {
            TipoRelacaoDAL = tipoRelacaoDAL;
            RegistroDAL = registroDAL;
            ReferenciaDAL = referenciaDAL;
        }
        public void VincularReferencia(PessoaDTO pessoaDTO)
        {
            var relacoes = ListarRelacoes((int)pessoaDTO.Codigo);

            foreach (var ralacao in relacoes)
                DataContext.Pessoaregistros.Remove(ralacao);

            foreach (var relacao in pessoaDTO.Relacoes)
                DataContext.Add(new Pessoaregistro()
                {
                    Pessoa = pessoaDTO.Codigo,
                    Registro = (int)relacao.RelacaoID,
                    Tiporelacao = (int)TipoRelacaoDAL.Consultar(new TipoRelacaoDTO()
                    {
                        Nome = relacao.TipoRelacao
                    }).Codigo
                });
            DataContext.SaveChanges();
        }
        public List<RelacaoDTO> ObterRelacao(int codPessoa)
        {
            var relacoes = (from relacao in DataContext.Pessoaregistros
                join
                    tiporelacao in DataContext.Tiporelacaos
                    on relacao.Tiporelacao equals tiporelacao.Codigo
                where 
                    relacao.Pessoa == codPessoa
                select 
                    new RelacaoDTO()
                    {
                        Codigo = relacao.Codigo,
                        RegistroPessoaID = (int)relacao.Pessoa,
                        RelacaoID = (int)relacao.Registro,
                        TipoRelacao = tiporelacao.Nome
                    }).AsNoTracking().ToList();
            return relacoes;
        }
        public List<RegistroDTO> ObterRelacaoCompleta(PessoaDTO pessoaDTO)
        {
            var registros = (from pessoaRelacao in DataContext.Pessoaregistros
                join         
                    registro in DataContext.Registros
                    on pessoaRelacao.Registro equals registro.Codigo
                join
                    idioma in DataContext.Idiomas
                    on registro.Idioma equals idioma.Codigo
                join
                    tipo in DataContext.Tipos
                    on registro.Tipo equals tipo.Codigo
                join
                    descricao in DataContext.Descricaos
                    on registro.Codigo equals descricao.Registro into descricaoLeftJoin from descricaoLeft in descricaoLeftJoin.DefaultIfEmpty()
                join
                    registroApelido in DataContext.Registroapelidos
                    on registro.Codigo equals registroApelido.Registro into registroApelidoLeftJoin from registroApelidoLeft in registroApelidoLeftJoin.DefaultIfEmpty()
                join
                   apelido in DataContext.Apelidos
                   on new Registroapelido(){ 
                       Apelido = registroApelidoLeft != null ? registroApelidoLeft.Apelido : 0
                    }.Apelido equals apelido.Codigo into apelidoLeftJoin from apelidoLeft in apelidoLeftJoin.DefaultIfEmpty()
                join
                    registroLocalizacao in DataContext.Registrolocalizacaos
                    on registro.Codigo equals registroLocalizacao.Registro into registroLocalizacaoLeftJoin from registroLocalizacaoLeft in registroLocalizacaoLeftJoin.DefaultIfEmpty()
                join
                   localizacaoGeografica in DataContext.Localizacaogeograficas
                   on new Registrolocalizacao(){ 
                       Localizacaogeografica = registroLocalizacaoLeft != null ? registroLocalizacaoLeft.Localizacaogeografica : 0
                    }.Localizacaogeografica equals localizacaoGeografica.Codigo into localizacaoGeograficaLeftJoin from localizacaoGeograficaLeft in localizacaoGeograficaLeftJoin.DefaultIfEmpty()

                where 
                    pessoaRelacao.Pessoa == pessoaDTO.Codigo
                
                select new RegistroDTO()
                {
                    Codigo = registro.Codigo,
                    Nome = registro.Nome,
                    Apelido = apelidoLeft != null ? apelidoLeft.Nome : string.Empty,
                    Idioma = idioma.Nome,
                    Tipo = tipo.Nome,
                    Conteudo = registro.Conteudo,
                    Descricao = descricaoLeft != null ? descricaoLeft.Conteudo : string.Empty,
                    DataInsercao = registro.Datainsercao,
                    Latitude = ObterLocalizacaoGeografica(localizacaoGeograficaLeft, true).ToString(),
                    Longitude = ObterLocalizacaoGeografica(localizacaoGeograficaLeft, false).ToString()
                }).AsNoTracking().DistinctBy(registroDB => registroDB.Codigo).ToList();
            
            foreach(var registro in registros)
                registro.Referencias = ReferenciaDAL.ObterReferencia(registro.Codigo);
            
            return registros;
        }
        private static double? ObterLocalizacaoGeografica(Localizacaogeografica localizacaoGeograficaLeft, bool latitude)
        {
            if (localizacaoGeograficaLeft != null)
                return latitude ? localizacaoGeograficaLeft.Latitude : localizacaoGeograficaLeft.Longitude;
            return null;
        }
        private IQueryable<Pessoaregistro> ListarRelacoes(int codPessoa)
        {
            return DataContext.Pessoaregistros.Where(relacao => relacao.Pessoa == codPessoa).AsNoTracking();
        }
    }
}