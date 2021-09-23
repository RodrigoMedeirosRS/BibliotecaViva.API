using System.Linq;
using Microsoft.EntityFrameworkCore;

using BibliotecaViva.DAO;
using BibliotecaViva.DTO;
using BibliotecaViva.DAL.Utils;
using BibliotecaViva.DAL.Interfaces;

namespace BibliotecaViva.DAL
{
    public class NomeSocialDAL : BaseDAL, INomeSocialDAL
    {
        public NomeSocialDAL(bibliotecavivaContext dataContext) : base(dataContext)
        {

        }

        public void Cadastrar(NomeSocialDTO nomeSocialDTO)
        {
            var nomeSocial = ValidarJaCadastrado(nomeSocialDTO);
            if (nomeSocial != null)
            {
                nomeSocial.Nome = nomeSocialDTO.Nome;
                DataContext.Update(nomeSocial);
                DataContext.SaveChanges();
            }
            else
            {
                DataContext.Add(Conversor.Mapear(nomeSocialDTO));
                DataContext.SaveChanges();
            }
        }
        
        public void Remover(int? codigoPessoa)
        {
            var nomesocial = DataContext.Nomesocials.AsNoTracking().FirstOrDefault(nomeSocial => nomeSocial.Pessoa == codigoPessoa);
            DataContext.Remove(nomesocial);
            DataContext.SaveChanges();
        }

        private Nomesocial ValidarJaCadastrado(NomeSocialDTO nomeSocialDTO)
        {
            return DataContext.Nomesocials.AsNoTracking().FirstOrDefault(nomeSocial => nomeSocial.Pessoa == nomeSocialDTO.Pessoa);
        }  
    }
}