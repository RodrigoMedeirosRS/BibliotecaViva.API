using BibliotecaViva.DTO;

namespace BibliotecaViva.DAL.Interfaces
{
    public interface INomeSocialDAL
    {
        void Cadastrar(NomeSocialDTO nomeSocial);
        void Remover(int? codigoPessoa);
    }
}