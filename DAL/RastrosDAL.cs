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
    public class RastrosDAL : BaseDAL, IRastrosDAL
    {
        public RastrosDAL(bibliotecavivaContext dataContext) : base(dataContext)
        {
        }
        public List<LocalizacaoGeograficaDTO> Consultar()
        {
            var resultado = ConsultarLocalizacaoPessoas();
            resultado.AddRange(ConsultarLocalizacaoRegistros());
            return resultado;
        }
        private List<LocalizacaoGeograficaDTO> ConsultarLocalizacaoPessoas()
        {
            return (from Localizacaogeografica in DataContext.Localizacaogeograficas.AsNoTracking()
                join   
                    registroLocalizacao in DataContext.Pessoalocalizacaos.AsNoTracking()
                    on Localizacaogeografica.Codigo equals registroLocalizacao.Localizacaogeografica
                select 
                    new LocalizacaoGeograficaDTO()
                    {
                        Codigo = Localizacaogeografica.Codigo,
                        Latitude = Localizacaogeografica.Latitude,
                        Longitude = Localizacaogeografica.Longitude
                    }).AsNoTracking().DistinctBy(localizacaoDB => localizacaoDB.Codigo).ToList();
        }
        private List<LocalizacaoGeograficaDTO> ConsultarLocalizacaoRegistros()
        {
            return (from Localizacaogeografica in DataContext.Localizacaogeograficas.AsNoTracking()
                join   
                    registroLocalizacao in DataContext.Registrolocalizacaos.AsNoTracking()
                    on Localizacaogeografica.Codigo equals registroLocalizacao.Localizacaogeografica
                select 
                    new LocalizacaoGeograficaDTO()
                    {
                        Codigo = Localizacaogeografica.Codigo,
                        Latitude = Localizacaogeografica.Latitude,
                        Longitude = Localizacaogeografica.Longitude
                    }).AsNoTracking().DistinctBy(localizacaoDB => localizacaoDB.Codigo).ToList();
        }
    }
}