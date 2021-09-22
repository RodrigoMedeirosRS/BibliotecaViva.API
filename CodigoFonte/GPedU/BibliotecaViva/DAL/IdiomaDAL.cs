using System.Linq;
using System.Collections.Generic;
using BibliotecaViva.DAO;
using BibliotecaViva.DTO;
using BibliotecaViva.DAL.Interfaces;

namespace BibliotecaViva.DAL
{
    public class IdiomaDAL : BaseDAL, IIdiomaDAL
    {
        public IdiomaDAL(ISQLiteDataContext dataContext) : base(dataContext)
        {
        }

        public void Cadastrar(IdiomaDTO idiomaDTO)
        {
            DataContext.ObterDataContext().InsertOrReplace(new Idioma()
            {
                Codigo = (int)ValidarJaCadastrado(idiomaDTO),
                Nome = idiomaDTO.Nome
            });
        }
        public IdiomaDTO Consultar(IdiomaDTO idiomaDTO)
        {
            var resultado = new Idioma();

            if (string.IsNullOrEmpty(idiomaDTO.Nome))
                resultado = DataContext.ObterDataContext().Table<Idioma>().FirstOrDefault(idioma => idioma.Codigo == idiomaDTO.Codigo);
            else
                resultado = DataContext.ObterDataContext().Table<Idioma>().FirstOrDefault(idioma => idioma.Nome == idiomaDTO.Nome);
            
            return Mapear<Idioma, IdiomaDTO>(resultado);
        }

        public List<IdiomaDTO> Listar()
        {
            return (from tipo in DataContext.ObterDataContext().Table<Idioma>() select new IdiomaDTO()
            {
                Codigo = tipo.Codigo,
                Nome = tipo.Nome
            }).ToList(); 
        }

        private int? ValidarJaCadastrado(IdiomaDTO idiomaDTO)
        {
            var resultado = DataContext.ObterDataContext().Table<Idioma>().FirstOrDefault(idioma => idioma.Nome == idiomaDTO.Nome);
            return resultado != null ? resultado.Codigo : null;
        }  
    }
}