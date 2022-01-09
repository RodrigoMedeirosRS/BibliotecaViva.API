using System.Linq;
using Microsoft.EntityFrameworkCore;

using BibliotecaViva.DAO;
using BibliotecaViva.DTO;
using BibliotecaViva.DAL.Utils;
using BibliotecaViva.DAL.Interfaces;

namespace BibliotecaViva.DAL
{
    public class LocalizacaoGeograficaDAL : BaseDAL, ILocalizacaoGeograficaDAL
    {
        public LocalizacaoGeograficaDAL(bibliotecavivaContext dataContext) : base(dataContext)
        {

        }

        public void Cadastrar(LocalizacaoGeograficaDTO localizacaoGeograficaDTO)
        {
            DataContext.Localizacaogeograficas.Add(Conversor.Mapear(localizacaoGeograficaDTO));
            DataContext.SaveChanges();
        }

        public void Vincular(LocalizacaoGeograficaDTO localizacaoGeograficaDTO, PessoaDTO pessoaDTO)
        {
            DataContext.Pessoalocalizacaos.Add(new Pessoalocalizacao()
            {
                Pessoa = (int)pessoaDTO.Codigo,
                Localizacaogeografica = (int)localizacaoGeograficaDTO.Codigo
            });
            DataContext.SaveChanges();
        }

        public void Vincular(LocalizacaoGeograficaDTO localizacaoGeograficaDTO, RegistroDTO registroDTO)
        {
            DataContext.Registrolocalizacaos.Add(new Registrolocalizacao()
            {
                Registro = (int)registroDTO.Codigo,
                Localizacaogeografica = (int)localizacaoGeograficaDTO.Codigo
            });
            DataContext.SaveChanges();
        }
        
        public void RemoverVinculoPessoa(int? codigoPessoa)
        {
            var localizacao = DataContext.Pessoalocalizacaos.AsNoTracking().FirstOrDefault(localizacao => localizacao.Pessoa == codigoPessoa);
            if (localizacao != null)
                DataContext.Remove(localizacao);
            DataContext.SaveChanges();
        }

        public void RemoverVinculoRegistro(int? codigoRegistro)
        {
            var localizacao = DataContext.Registrolocalizacaos.AsNoTracking().FirstOrDefault(localizacao => localizacao.Registro == codigoRegistro);
            DataContext.Remove(localizacao);
            DataContext.SaveChanges();
        }  
    }
}