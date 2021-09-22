using BibliotecaViva.DAO;
using BibliotecaViva.DTO;
using BibliotecaViva.DAL.Interfaces;

namespace BibliotecaViva.DAL
{
    public class NomeSocialDAL : BaseDAL, INomeSocialDAL
    {
        public NomeSocialDAL(ISQLiteDataContext dataContext) : base(dataContext)
        {
        }

        public void Cadastrar(NomeSocialDTO nomeSocial)
        {
            nomeSocial.Codigo = ValidarJaCadastrado(nomeSocial);
            DataContext.ObterDataContext().InsertOrReplace(Mapear<NomeSocialDTO, Nomesocial>(nomeSocial));
        }
        
        public void Remover(int? codigoPessoa)
        {
            var resultado = DataContext.ObterDataContext().Table<Nomesocial>().FirstOrDefault(nomeSocial => nomeSocial.Pessoa == codigoPessoa);
            if (resultado != null)
                DataContext.ObterDataContext().Delete(resultado);
        }

        private int? ValidarJaCadastrado(NomeSocialDTO nomeSocialDTO)
        {
            var resultado = DataContext.ObterDataContext().Table<Nomesocial>().FirstOrDefault(nomeSocial => nomeSocial.Pessoa == nomeSocialDTO.Pessoa);
            return resultado != null ? resultado.Codigo : null;
        }  
    }
}