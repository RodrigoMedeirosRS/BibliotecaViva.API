using BibliotecaViva.DAO;
using BibliotecaViva.DTO;
using BibliotecaViva.DAL.Interfaces;

namespace BibliotecaViva.DAL
{
    public class LocalizacaoGeograficaDAL : BaseDAL, ILocalizacaoGeograficaDAL
    {
        public LocalizacaoGeograficaDAL(ISQLiteDataContext dataContext) : base(dataContext)
        {

        }

        public void Cadastrar(LocalizacaoGeograficaDTO localizacaoGeograficaDTO)
        {
            localizacaoGeograficaDTO.Codigo = ValidarJaCadastrado(localizacaoGeograficaDTO);
            DataContext.ObterDataContext().InsertOrReplace(Mapear<LocalizacaoGeograficaDTO, Localizacaogeografica>(localizacaoGeograficaDTO));
        }

        public void Vincular(LocalizacaoGeograficaDTO localizacaoGeograficaDTO, PessoaDTO pessoaDTO)
        {
            localizacaoGeograficaDTO.Codigo = ValidarJaCadastrado(localizacaoGeograficaDTO);
            if (localizacaoGeograficaDTO.Codigo != null)
                DataContext.ObterDataContext().InsertOrReplace(new Pessoalocalizacao()
                {
                    Pessoa = (int)pessoaDTO.Codigo,
                    Localizacaogeografica = (int)localizacaoGeograficaDTO.Codigo
                });
        }

        public void Vincular(LocalizacaoGeograficaDTO localizacaoGeograficaDTO, RegistroDTO registroDTO)
        {
            localizacaoGeograficaDTO.Codigo = ValidarJaCadastrado(localizacaoGeograficaDTO);
            if (localizacaoGeograficaDTO.Codigo != null)
                DataContext.ObterDataContext().InsertOrReplace(new Registrolocalizacao()
                {
                    Registro = (int)registroDTO.Codigo,
                    Localizacaogeografica = (int)localizacaoGeograficaDTO.Codigo
                });
        }
        
        public void RemoverVinculoPessoa(int? codigoPessoa)
        {
            var resultado = DataContext.ObterDataContext().Table<Pessoalocalizacao>().FirstOrDefault(localizacaoGeografica => localizacaoGeografica.Pessoa == codigoPessoa);
            if (resultado != null)
                DataContext.ObterDataContext().Delete(resultado);
        }

        public void RemoverVinculoRegistro(int? codigoRegistro)
        {
            var resultado = DataContext.ObterDataContext().Table<Registroapelido>().FirstOrDefault(localizacaoGeografica => localizacaoGeografica.Registro == codigoRegistro);
            if (resultado != null)
                DataContext.ObterDataContext().Delete(resultado);
        }

        private int? ValidarJaCadastrado(LocalizacaoGeograficaDTO localizacaoGeograficaDTO)
        {
            var resultado = DataContext.ObterDataContext().Table<Localizacaogeografica>().
                FirstOrDefault(localizacaoGeografica => 
                    localizacaoGeografica.Latitude == localizacaoGeograficaDTO.Latitude &&
                    localizacaoGeografica.Longitude == localizacaoGeograficaDTO.Longitude);
            return resultado != null ? resultado.Codigo : null;
        }  
    }
}