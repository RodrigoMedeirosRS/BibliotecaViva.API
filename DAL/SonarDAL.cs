using System;
using MoreLinq;
using System.Linq;
using System.Globalization;
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
        public SonarDAL(bibliotecavivaContext dataContext, IPessoaDAL pessoaDAL, IRegistroDAL registroDAL) : base(dataContext)
        {
            PessoaDAL = pessoaDAL;
            RegistroDAL = registroDAL;
        }
        public SonarRetorno Consultar(SonarDTO sonar)
        {
            return new SonarRetorno()
            {
                Registros = PopularRegistros(BuscarRegistros(sonar))
            };
        }
        private List<RegistroDTO> PopularRegistros(List<RegistroDTO> registros)
        {
            var retorno = new List<RegistroDTO>();
            
            foreach(var registro in registros)
                retorno.AddRange(RegistroDAL.Consultar(registro));

            return retorno.DistinctBy(registro => registro.Codigo).ToList();
        }

        private List<RegistroDTO> BuscarRegistros(SonarDTO sonar)
        {
            return(from registroLocalizacao in DataContext.Registrolocalizacaos.AsNoTracking()
                join
                    registro in DataContext.Registros.AsNoTracking()
                    on registroLocalizacao.Registro equals registro.Codigo
                join
                    localizacaoGeografica in DataContext.Localizacaogeograficas.AsNoTracking()
                    on registroLocalizacao.Localizacaogeografica equals localizacaoGeografica.Codigo
                join
                    idioma in DataContext.Idiomas.AsNoTracking()
                    on registro.Idioma equals idioma.Codigo
                where 
                    (Math.Sqrt(Math.Pow((sonar.Latitude - localizacaoGeografica.Latitude), 2) + Math.Pow((sonar.Longitude - localizacaoGeografica.Longitude), 2))) <= sonar.Alcance
                select new RegistroDTO()
                {
                    Codigo = registro.Codigo,
                    Nome = registro.Nome,
                    Idioma = idioma.Nome
                }).AsNoTracking().ToList();
        }  
    }
}