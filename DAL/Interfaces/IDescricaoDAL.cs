using BibliotecaViva.DTO;

namespace BibliotecaViva.DAL.Interfaces
{
    public interface IDescricaoDAL
    {
        void Cadastrar(DescricaoDTO descricaoDTO);
        void Remover(int? codigoRegistro);
    }
}