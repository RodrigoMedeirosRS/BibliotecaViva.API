using BibliotecaViva.DAO;
using BibliotecaViva.DTO;

namespace BibliotecaViva.DAL.Utils
{
    internal static class Conversor
    {
        internal static Apelido Mapear(ApelidoDTO apelido)
        {
            return new Apelido()
            {
                Codigo = (int)apelido.Codigo,
                Nome = apelido.Nome
            };
        }
        internal static ApelidoDTO Mapear(Apelido apelido)
        {
            return new ApelidoDTO()
            {
                Codigo = apelido.Codigo,
                Nome = apelido.Nome
            };
        }
        internal static Descricao Mapear(DescricaoDTO descricao)
        {
            return new Descricao()
            {
                Registro = (int)descricao.Registro,
                Descricao1 = descricao.Conteudo
            };
        }
        internal static DescricaoDTO Mapear(Descricao descricao)
        {
            return new DescricaoDTO()
            {
                Registro = (int)descricao.Registro,
                Conteudo = descricao.Descricao1
            };
        }
        internal static Idioma Mapear(IdiomaDTO idioma)
        {
            return new Idioma()
            {
                Codigo = (int)idioma.Codigo,
                Nome = idioma.Nome
            };
        }
        internal static IdiomaDTO Mapear(Idioma idioma)
        {
            return new IdiomaDTO()
            {
                Codigo = idioma.Codigo,
                Nome = idioma.Nome
            };
        }
        internal static Tipo Mapear(TipoDTO tipo)
        {
            return new Tipo()
            {
                Codigo = (int)tipo.Codigo,
                Nome = tipo.Nome,
                Extensao = tipo.Extensao
            };
        }
        internal static TipoDTO Mapear(Tipo tipo)
        {
            return new TipoDTO()
            {
                Codigo = tipo.Codigo,
                Nome = tipo.Nome,
                Extensao = tipo.Extensao
            };
        }
        internal static Localizacaogeografica Mapear(LocalizacaoGeograficaDTO localizacaoGeografica)
        {
            return new Localizacaogeografica()
            {
                Codigo = (int) localizacaoGeografica.Codigo,
                Latitude = localizacaoGeografica.Latitude,
                Longitude = localizacaoGeografica.Longitude
            };
        }
        internal static LocalizacaoGeograficaDTO Mapear(Localizacaogeografica localizacaoGeografica)
        {
            return new LocalizacaoGeograficaDTO()
            {
                Codigo = localizacaoGeografica.Codigo,
                Latitude = localizacaoGeografica.Latitude,
                Longitude = localizacaoGeografica.Longitude
            };
        }
    }
}