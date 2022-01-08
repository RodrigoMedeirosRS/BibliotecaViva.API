using BibliotecaViva.DAO;
using BibliotecaViva.DTO;

namespace BibliotecaViva.DAL.Utils
{
    internal static class Conversor
    {
        internal static Apelido Mapear(ApelidoDTO apelido)
        {
            return apelido != null ? new Apelido()
            {
                Codigo = apelido.Codigo,
                Nome = apelido.Nome
            } : null;
        }
        internal static ApelidoDTO Mapear(Apelido apelido)
        {
            return apelido != null ? new ApelidoDTO()
            {
                Codigo = apelido.Codigo,
                Nome = apelido.Nome
            } : null;
        }
        internal static Descricao Mapear(DescricaoDTO descricao)
        {
            return descricao != null ? new Descricao()
            {
                Registro = descricao.Registro,
                Conteudo = descricao.Conteudo
            } : null;
        }
        internal static DescricaoDTO Mapear(Descricao descricao)
        {
            return descricao != null ? new DescricaoDTO()
            {
                Registro = descricao.Registro,
                Conteudo = descricao.Conteudo
            } : null;
        }
        internal static Idioma Mapear(IdiomaDTO idioma)
        {
            return idioma != null ? new Idioma()
            {
                Codigo = idioma.Codigo,
                Nome = idioma.Nome
            } : null;
        }
        internal static IdiomaDTO Mapear(Idioma idioma)
        {
            return idioma != null ? new IdiomaDTO()
            {
                Codigo = idioma.Codigo,
                Nome = idioma.Nome
            } : null;
        }
        internal static Tipo Mapear(TipoDTO tipo)
        {
            return tipo != null ? new Tipo()
            {
                Codigo = tipo.Codigo,
                Nome = tipo.Nome,
                Extensao = tipo.Extensao,
                Binario = tipo.Binario
            } : null;
        }
        internal static TipoDTO Mapear(Tipo tipo)
        {
            return tipo != null ? new TipoDTO()
            {
                Codigo = tipo.Codigo,
                Nome = tipo.Nome,
                Extensao = tipo.Extensao,
                Binario = tipo.Binario
            } : null;
        }
        internal static Localizacaogeografica Mapear(LocalizacaoGeograficaDTO localizacaoGeografica)
        {
            return localizacaoGeografica != null ? new Localizacaogeografica()
            {
                Codigo = localizacaoGeografica.Codigo,
                Latitude = localizacaoGeografica.Latitude,
                Longitude = localizacaoGeografica.Longitude
            } : null;
        }
        internal static LocalizacaoGeograficaDTO Mapear(Localizacaogeografica localizacaoGeografica)
        {
            return localizacaoGeografica != null ? new LocalizacaoGeograficaDTO()
            {
                Codigo = localizacaoGeografica.Codigo,
                Latitude = localizacaoGeografica.Latitude,
                Longitude = localizacaoGeografica.Longitude
            } : null;
        }
        internal static Nomesocial Mapear(NomeSocialDTO nomeSocial)
        {
            return nomeSocial != null ? new Nomesocial()
            {
                Codigo = nomeSocial.Codigo,
                Nome = nomeSocial.Nome,
                Pessoa = nomeSocial.Pessoa  
            } : null;
        }
        internal static NomeSocialDTO Mapear(Nomesocial nomeSocial)
        {
            return nomeSocial != null ? new NomeSocialDTO()
            {
                Codigo = nomeSocial.Codigo,
                Nome = nomeSocial.Nome,
                Pessoa = nomeSocial.Pessoa  
            } : null;
        }
        internal static Pessoa Mapear(PessoaDTO pessoa)
        {
            return pessoa != null ? new Pessoa()
            {
                Codigo = pessoa.Codigo,
                Nome = pessoa.Nome,
                Sobrenome = pessoa.Sobrenome,
                Genero = pessoa.Genero
            } : null;
        }
        internal static PessoaDTO Mapear(Pessoa pessoa)
        {
            return pessoa != null ? new PessoaDTO()
            {
                Codigo = pessoa.Codigo,
                Nome = pessoa.Nome,
                Sobrenome = pessoa.Sobrenome,
                Genero = pessoa.Genero
            } : null;
        }
        internal static Tiporelacao Mapear(TipoRelacaoDTO tipoRelacao)
        {
            return tipoRelacao != null ? new Tiporelacao()
            {
                Codigo = tipoRelacao.Codigo,
                Nome = tipoRelacao.Nome
            } : null;
        }
        internal static TipoRelacaoDTO Mapear(Tiporelacao tipoRelacao)
        {
            return tipoRelacao != null ? new TipoRelacaoDTO()
            {
                Codigo = tipoRelacao.Codigo,
                Nome = tipoRelacao.Nome
            } : null;
        }
    }
}