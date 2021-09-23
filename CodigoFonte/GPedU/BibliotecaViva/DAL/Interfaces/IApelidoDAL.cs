using BibliotecaViva.DTO;

namespace BibliotecaViva.DAL.Interfaces
{
    public interface IApelidoDAL
    {
        void Cadastrar(ApelidoDTO apelidoDTO);
        void RemoverVinculo(int codigoPessoa);
        void RemoverVinculoRegistro(int codigoRegistro);
        void VincularPessoa(ApelidoDTO apelidoDTO, PessoaDTO pessoaDTO);
        void VincularRegistro(ApelidoDTO apelidoDTO, RegistroDTO registroDTO);
    }
}