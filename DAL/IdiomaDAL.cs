using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

using BibliotecaViva.DAO;
using BibliotecaViva.DTO;
using BibliotecaViva.DAL.Utils;
using BibliotecaViva.DAL.Interfaces;

namespace BibliotecaViva.DAL
{
    public class IdiomaDAL : BaseDAL, IIdiomaDAL
    {
        public IdiomaDAL(bibliotecavivaContext dataContext) : base(dataContext)
        {
        }
        public void Cadastrar(IdiomaDTO idiomaDTO)
        {
            if (Consultar(idiomaDTO) == null)
            {
                DataContext.Add(Conversor.Mapear(idiomaDTO));
                DataContext.SaveChanges();
            };
        }
        public IdiomaDTO Consultar(IdiomaDTO idiomaDTO)
        {
            var resultado = new Idioma();

            if (string.IsNullOrEmpty(idiomaDTO.Nome))
                resultado = DataContext.Idiomas.AsNoTracking().FirstOrDefault(idioma => idioma.Codigo == idiomaDTO.Codigo);
            else
                resultado = DataContext.Idiomas.AsNoTracking().FirstOrDefault(idioma => idioma.Nome.ToLower() == idiomaDTO.Nome.ToLower());
            
            return Conversor.Mapear(resultado);
        }
        public List<IdiomaDTO> Listar()
        {
            return (from idioma in DataContext.Idiomas
                orderby
                    idioma.Nome 
                select 
                    Conversor.Mapear(idioma)).ToList(); 
        }  
    }
}