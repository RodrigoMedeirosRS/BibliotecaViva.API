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
                Conteudo = descricao.Conteudo
            };
        }
        internal static DescricaoDTO Mapear(Descricao descricao)
        {
            return new DescricaoDTO()
            {
                Registro = (int)descricao.Registro,
                Conteudo = descricao.Conteudo
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
        internal static Nomesocial Mapear(NomeSocialDTO nomeSocial)
        {
            return new Nomesocial()
            {
                Codigo = (int)nomeSocial.Codigo,
                Nome = nomeSocial.Nome,
                Pessoa = (int)nomeSocial.Pessoa  
            };
        }
        internal static NomeSocialDTO Mapear(Nomesocial nomeSocial)
        {
            return new NomeSocialDTO()
            {
                Codigo = nomeSocial.Codigo,
                Nome = nomeSocial.Nome,
                Pessoa = nomeSocial.Pessoa  
            };
        }
        internal static Pessoa Mapear(PessoaDTO pessoa)
        {
            return new Pessoa()
            {
                Codigo = (int)pessoa.Codigo,
                Nome = pessoa.Nome,
                Sobrenome = pessoa.Sobrenome,
                Genero = pessoa.Genero
            };
        }
        internal static PessoaDTO Mapear(Pessoa pessoa)
        {
            return new PessoaDTO()
            {
                Codigo = pessoa.Codigo,
                Nome = pessoa.Nome,
                Sobrenome = pessoa.Sobrenome,
                Genero = pessoa.Genero
            };
        }
        internal static Tiporelacao Mapear(TipoRelacaoDTO tipoRelacao)
        {
            return new Tiporelacao()
            {
                Codigo = (int)tipoRelacao.Codigo,
                Nome = tipoRelacao.Nome
            };
        }
        internal static TipoRelacaoDTO Mapear(Tiporelacao tipoRelacao)
        {
            return new TipoRelacaoDTO()
            {
                Codigo = tipoRelacao.Codigo,
                Nome = tipoRelacao.Nome
            };
        }
    }
}