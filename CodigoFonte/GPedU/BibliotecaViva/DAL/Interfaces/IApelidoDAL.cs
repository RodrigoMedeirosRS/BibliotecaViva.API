using BibliotecaViva.DTO;

namespace BibliotecaViva.DAL.Interfaces
{
    public interface IApelidoDAL : IBaseDAL
    {
        void Cadastrar(ApelidoDTO apelidoDTO);
        void RemoverVinculoPessoa(int? codigoPessoa);
        void RemoverVinculoRegistro(int? codigoRegistro);
        void VincularPessoaApelido(ApelidoDTO apelidoDTO, PessoaDTO pessoaDTO);
        void VincularRegistroApelido(ApelidoDTO apelidoDTO, RegistroDTO registroDTO);
    }
}