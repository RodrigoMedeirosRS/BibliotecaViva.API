using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

using BibliotecaViva.DAO;
using BibliotecaViva.DTO;
using BibliotecaViva.DAL.Utils;
using BibliotecaViva.DAL.Interfaces;

namespace BibliotecaViva.DAL
{
    public class TipoDAL : BaseDAL, ITipoDAL
    {
        public TipoDAL(bibliotecavivaContext dataContext) : base(dataContext)
        {
        }

        public void Cadastrar(TipoDTO tipoDTO)
        {
            if(Consultar(tipoDTO) == null)
            {
                DataContext.Tipos.Add(Conversor.Mapear(tipoDTO));
                DataContext.SaveChanges();
            }
        }  
        public TipoDTO Consultar(TipoDTO tipoDTO)
        {
            var resultado = new Tipo();

            if (string.IsNullOrEmpty(tipoDTO.Nome))
                resultado = DataContext.Tipos.AsNoTracking().FirstOrDefault(tipo => tipo.Codigo == tipoDTO.Codigo);
            else
                resultado = DataContext.Tipos.AsNoTracking().FirstOrDefault(tipo => tipo.Nome.ToLower() == tipoDTO.Nome.ToLower());
            
            return Conversor.Mapear(resultado);
        }
        public List<TipoDTO> Listar()
        {
            return (from tipo in DataContext.Tipos 
            orderby
                tipo.Nome
            select 
                Conversor.Mapear(tipo)).ToList(); 
        } 
    }
}