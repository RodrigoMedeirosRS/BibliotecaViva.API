using System;
using MoreLinq;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

using BibliotecaViva.DTO;
using BibliotecaViva.DAO;
using BibliotecaViva.DTO.Dominio;
using BibliotecaViva.DAL.Interfaces;


namespace BibliotecaViva.DAL
{
    public class SonarDAL : BaseDAL, ISonarDAL
    {
        private IPessoaDAL PessoaDAL { get; set; }
        private IRegistroDAL RegistroDAL { get; set; }
        public SonarDAL(bibliotecavivaContext dataContext) : base(dataContext)
        {
        }
        public SonarRetorno Consultar(SonarConsulta sonar)
        {
            return new SonarRetorno()
            {
                Pessoas = PopularPessoas(BuscarPessoas(sonar)),
                Registros = PopularRegistros(BuscarRegistros(sonar))
            };
        }

        private List<PessoaDTO> PopularPessoas(List<PessoaDTO> pessoas)
        {
            var retorno = new List<PessoaDTO>();
            
            foreach(var pessoa in pessoas)
                retorno.AddRange(PessoaDAL.Consultar(pessoa));

            return retorno.DistinctBy(pessoa => pessoa.Codigo).ToList();
        }

        private List<PessoaDTO> BuscarPessoas(SonarConsulta sonar)
        {
            return(from pessoaLocalizacao in DataContext.Pessoalocalizacaos
                join
                    pessoa in DataContext.Pessoas
                    on pessoaLocalizacao.Pessoa equals pessoa.Codigo
                join
                    localizacaoGeografica in DataContext.Localizacaogeograficas
                    on pessoaLocalizacao.Localizacaogeografica equals localizacaoGeografica.Codigo
                where localizacaoGeografica.Latitude >= sonar.CoordenadaInicio[0] && 
                    localizacaoGeografica.Latitude <= sonar.CoordenadaFim[0] &&
                    localizacaoGeografica.Longitude >= sonar.CoordenadaInicio[1] &&
                    localizacaoGeografica.Longitude <= sonar.CoordenadaFim[1]
                select new PessoaDTO()
                {
                    Codigo = pessoa.Codigo,
                    Nome = pessoa.Nome,
                    Sobrenome = pessoa.Sobrenome
                }).AsNoTracking().ToList();
        }

        private List<RegistroDTO> PopularRegistros(List<RegistroDTO> registros)
        {
            var retorno = new List<RegistroDTO>();
            
            foreach(var registro in registros)
                retorno.AddRange(RegistroDAL.Consultar(registro));

            return retorno.DistinctBy(registro => registro.Codigo).ToList();
        }

        private List<RegistroDTO> BuscarRegistros(SonarConsulta sonar)
        {
            return(from registroLocalizacao in DataContext.Registrolocalizacaos
                join
                    registro in DataContext.Registros
                    on registroLocalizacao.Registro equals registro.Codigo
                join
                    localizacaoGeografica in DataContext.Localizacaogeograficas
                    on registroLocalizacao.Localizacaogeografica equals localizacaoGeografica.Codigo
                join
                    idioma in DataContext.Idiomas
                    on registro.Idioma equals idioma.Codigo
                where localizacaoGeografica.Latitude >= sonar.CoordenadaInicio[0] && 
                    localizacaoGeografica.Latitude <= sonar.CoordenadaFim[0] &&
                    localizacaoGeografica.Longitude >= sonar.CoordenadaInicio[1] &&
                    localizacaoGeografica.Longitude <= sonar.CoordenadaFim[1]
                select new RegistroDTO()
                {
                    Codigo = registro.Codigo,
                    Nome = registro.Nome,
                    Idioma = idioma.Nome
                }).AsNoTracking().ToList();
        }  
    }
}