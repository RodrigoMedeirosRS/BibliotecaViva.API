using MoreLinq;
using System.Linq;
using System.Collections.Generic;
using BibliotecaViva.DAO;
using BibliotecaViva.DTO;
using BibliotecaViva.DAL.Interfaces;

namespace BibliotecaViva.DAL
{
    public class PessoaDAL : BaseDAL, IPessoaDAL
    {
        private INomeSocialDAL NomeSocialDAL { get; set; }
        private IApelidoDAL ApelidoDAL { get; set; }
        private ILocalizacaoGeograficaDAL LocalizacaoGeograficaDAL { get; set; }
        private IPessoaRegistroDAL PessoaRegistroDAL { get; set; }
        
        public PessoaDAL(ISQLiteDataContext dataContext, INomeSocialDAL nomeSocialDAL, IApelidoDAL apelidoDAL, ILocalizacaoGeograficaDAL localizacaoGeograficaDAL, IPessoaRegistroDAL pessoaRegistroDAL) : base(dataContext)
        {
            ApelidoDAL = apelidoDAL;
            NomeSocialDAL = nomeSocialDAL;
            PessoaRegistroDAL = pessoaRegistroDAL;
            LocalizacaoGeograficaDAL = localizacaoGeograficaDAL;
        }

        public void Cadastrar(PessoaDTO pessoaDTO)
        {
            DataContext.ObterDataContext().InsertOrReplace(Mapear<PessoaDTO, Pessoa>(pessoaDTO)); 
            CadastrarDadosOpcionais(pessoaDTO);
        }

        public List<PessoaDTO> Consultar(PessoaDTO pessoaDTO)
        {
            return (from pessoa in DataContext.ObterDataContext().Table<Pessoa>()
                join
                    nomeSocial in DataContext.ObterDataContext().Table<Nomesocial>()
                    on pessoa.Codigo equals nomeSocial.Pessoa into nomeSocialLeftJoin from nomeSocialLeft in nomeSocialLeftJoin.DefaultIfEmpty()
                join
                    pessoaApelido in DataContext.ObterDataContext().Table<Pessoaapelido>()
                    on pessoa.Codigo equals pessoaApelido.Pessoa into pessoaApelidoLeftJoin from pessoaApelidoLeft in pessoaApelidoLeftJoin.DefaultIfEmpty()
                join
                   apelido in DataContext.ObterDataContext().Table<Apelido>()
                   on new Pessoaapelido(){ 
                       Apelido = pessoaApelidoLeft != null ? pessoaApelidoLeft.Apelido : 0
                    }.Apelido equals apelido.Codigo into apelidoLeftJoin from apelidoLeft in apelidoLeftJoin.DefaultIfEmpty()
                join
                    pessoaLocalizacao in DataContext.ObterDataContext().Table<Pessoalocalizacao>()
                    on pessoa.Codigo equals pessoaLocalizacao.Pessoa into pessoaLocalizacaoLeftJoin from pessoaLocalizacaoLeft in pessoaLocalizacaoLeftJoin.DefaultIfEmpty()
                join
                   localizacaoGeografica in DataContext.ObterDataContext().Table<Localizacaogeografica>()
                   on new Pessoalocalizacao(){ 
                       Localizacaogeografica = pessoaLocalizacaoLeft != null ? pessoaLocalizacaoLeft.LocalizacaoGeografica : 0
                    }.LocalizacaoGeografica equals localizacaoGeografica.Codigo into localizacaoGeograficaLeftJoin from localizacaoGeograficaLeft in localizacaoGeograficaLeftJoin.DefaultIfEmpty()
                
                where pessoa.Nome == pessoaDTO.Nome && pessoa.Sobrenome == pessoaDTO.Sobrenome
                
                select new PessoaDTO()
                {
                    Codigo = pessoa.Codigo,
                    Nome = pessoa.Nome,
                    Sobrenome = pessoa.Sobrenome,
                    Genero = pessoa.Genero,
                    Apelido = apelidoLeft != null ? apelidoLeft.Nome : string.Empty,
                    NomeSocial = nomeSocialLeft != null ? nomeSocialLeft.Nome : string.Empty,
                    Latitude = ObterLocalizacaoGeorafica(localizacaoGeograficaLeft, true),
                    Longitude = ObterLocalizacaoGeorafica(localizacaoGeograficaLeft, false),
                    Relacoes = PessoaRegistroDAL.ObterRelacao((int)pessoa.Codigo)
                }).DistinctBy(pessoaDB => pessoaDB.Codigo).ToList();
        }

        private double? ObterLocalizacaoGeorafica(Localizacaogeografica localizacaoGeograficaLeft, bool latitude)
        {
            if (localizacaoGeograficaLeft != null)
                return latitude ? localizacaoGeograficaLeft.Latitude : localizacaoGeograficaLeft.Longitude;
            return null;
        }

        private List<PessoaDTO> MapearPessoas(List<Pessoa> pessoas)
        {
            var retorno = new List<PessoaDTO>();

            foreach(var pessoa in pessoas)
                retorno.Add(Mapear<Pessoa, PessoaDTO>(pessoa));

            return retorno;
        }

        private PessoaDTO PopularCodigo(PessoaDTO pessoaDTO)
        {
            if (pessoaDTO.Codigo == null)
                pessoaDTO.Codigo = Consultar(pessoaDTO).FirstOrDefault().Codigo;
            return pessoaDTO;
        }

        private void CadastrarDadosOpcionais(PessoaDTO pessoaDTO)
        {
            pessoaDTO = PopularCodigo(pessoaDTO);
            CadastrarNomeSocial(pessoaDTO);
            CadastrarApelido(pessoaDTO);
            CadastrarLocalizacaoGeografica(pessoaDTO);
        }

        private void CadastrarNomeSocial(PessoaDTO pessoaDTO)
        {
            if (string.IsNullOrEmpty(pessoaDTO.NomeSocial))
                NomeSocialDAL.Remover(pessoaDTO.Codigo);
            else
                NomeSocialDAL.Cadastrar(new NomeSocialDTO()
                {
                    Pessoa = pessoaDTO.Codigo,
                    Nome = pessoaDTO.NomeSocial
                });
        }

        private void CadastrarApelido(PessoaDTO pessoaDTO)
        {
            if (string.IsNullOrEmpty(pessoaDTO.Apelido))
                ApelidoDAL.RemoverVinculo(pessoaDTO.Codigo);
            else
            {
                var apelidoDTO = new ApelidoDTO()
                { 
                    Nome = pessoaDTO.Apelido 
                };
                
                ApelidoDAL.Cadastrar(apelidoDTO);
                ApelidoDAL.VincularPessoa(apelidoDTO, pessoaDTO);
            }     
        }

        private void CadastrarLocalizacaoGeografica(PessoaDTO pessoaDTO)
        {
            if (pessoaDTO.Latitude == null || pessoaDTO.Longitude == null)
                LocalizacaoGeograficaDAL.RemoverVinculoPessoa(pessoaDTO.Codigo);
            else
            {
                var localizacaoGeograficaDTO = new LocalizacaoGeograficaDTO()
                { 
                    Latitude = (double)pessoaDTO.Latitude,
                    Longitude = (double)pessoaDTO.Longitude,
                };
                
                LocalizacaoGeograficaDAL.Cadastrar(localizacaoGeograficaDTO);
                LocalizacaoGeograficaDAL.Vincular(localizacaoGeograficaDTO, pessoaDTO);
            }     
        }
    }
}