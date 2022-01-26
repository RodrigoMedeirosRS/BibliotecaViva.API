using BibliotecaViva.DTO;

namespace BibliotecaViva.DAL.Interfaces
{
    public interface ILocalizacaoGeograficaDAL
    {
        void Cadastrar(LocalizacaoGeograficaDTO localizacaoGeograficaDTO);
        void RemoverVinculoRegistro(int? codigoRegistro);
        void Vincular(LocalizacaoGeograficaDTO localizacaoGeograficaDTO, RegistroDTO registroDTO);
    }
}