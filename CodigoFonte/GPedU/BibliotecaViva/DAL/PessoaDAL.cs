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
    public class PessoaDAL : BaseDAL, IPessoaDAL
    {
        private INomeSocialDAL NomeSocialDAL { get; set; }
        private IApelidoDAL ApelidoDAL { get; set; }
        private ILocalizacaoGeograficaDAL LocalizacaoGeograficaDAL { get; set; }
        private IPessoaRegistroDAL PessoaRegistroDAL { get; set; }
        
        public PessoaDAL(bibliotecavivaContext dataContext, INomeSocialDAL nomeSocialDAL, IApelidoDAL apelidoDAL, ILocalizacaoGeograficaDAL localizacaoGeograficaDAL, IPessoaRegistroDAL pessoaRegistroDAL) : base(dataContext)
        {
            ApelidoDAL = apelidoDAL;
            NomeSocialDAL = nomeSocialDAL;
            PessoaRegistroDAL = pessoaRegistroDAL;
            LocalizacaoGeograficaDAL = localizacaoGeograficaDAL;
        }

        public void Cadastrar(PessoaDTO pessoaDTO)
        {
            var pessoa = DataContext.Pessoas.AsNoTracking().FirstOrDefault(pessoa => pessoa.Codigo == pessoaDTO.Codigo);
            if(pessoa != null)
            {
                pessoa.Nome = pessoaDTO.Nome;
                pessoa.Sobrenome = pessoaDTO.Sobrenome;
                pessoa.Genero = pessoaDTO.Genero;
                DataContext.Update(pessoa);
                DataContext.SaveChanges();
            }
            else
            {
                DataContext.Add(Conversor.Mapear(pessoa));
                DataContext.SaveChanges();
            }
            CadastrarDadosOpcionais(pessoaDTO);
        }

        public List<PessoaDTO> Consultar(PessoaDTO pessoaDTO)
        {
            return (from pessoa in DataContext.Pessoas
                join
                    nomeSocial in DataContext.Nomesocials
                    on pessoa.Codigo equals nomeSocial.Pessoa into nomeSocialLeftJoin from nomeSocialLeft in nomeSocialLeftJoin.DefaultIfEmpty()
                join
                    pessoaApelido in DataContext.Pessoaapelidos
                    on pessoa.Codigo equals pessoaApelido.Pessoa into pessoaApelidoLeftJoin from pessoaApelidoLeft in pessoaApelidoLeftJoin.DefaultIfEmpty()
                join
                   apelido in DataContext.Apelidos
                   on new Pessoaapelido(){ 
                       Apelido = pessoaApelidoLeft != null ? pessoaApelidoLeft.Apelido : 0
                    }.Apelido equals apelido.Codigo into apelidoLeftJoin from apelidoLeft in apelidoLeftJoin.DefaultIfEmpty()
                join
                    pessoaLocalizacao in DataContext.Pessoalocalizacaos
                    on pessoa.Codigo equals pessoaLocalizacao.Pessoa into pessoaLocalizacaoLeftJoin from pessoaLocalizacaoLeft in pessoaLocalizacaoLeftJoin.DefaultIfEmpty()
                join
                   localizacaoGeografica in DataContext.Localizacaogeograficas
                   on new Pessoalocalizacao(){ 
                       Localizacaogeografica = pessoaLocalizacaoLeft != null ? pessoaLocalizacaoLeft.Localizacaogeografica : 0
                    }.Localizacaogeografica equals localizacaoGeografica.Codigo into localizacaoGeograficaLeftJoin from localizacaoGeograficaLeft in localizacaoGeograficaLeftJoin.DefaultIfEmpty()
                
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

        private long? ObterLocalizacaoGeorafica(Localizacaogeografica localizacaoGeograficaLeft, bool latitude)
        {
            if (localizacaoGeograficaLeft != null)
                return latitude ? localizacaoGeograficaLeft.Latitude : localizacaoGeograficaLeft.Longitude;
            return null;
        }

        private List<PessoaDTO> MapearPessoas(List<Pessoa> pessoas)
        {
            var retorno = new List<PessoaDTO>();

            foreach(var pessoa in pessoas)
                retorno.Add(Conversor.Mapear(pessoa));

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
                    Latitude = (long)pessoaDTO.Latitude,
                    Longitude = (long)pessoaDTO.Longitude,
                };
                
                LocalizacaoGeograficaDAL.Cadastrar(localizacaoGeograficaDTO);
                LocalizacaoGeograficaDAL.Vincular(localizacaoGeograficaDTO, pessoaDTO);
            }     
        }
    }
}