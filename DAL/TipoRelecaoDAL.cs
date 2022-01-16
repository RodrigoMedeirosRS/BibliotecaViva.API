using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

using BibliotecaViva.DAO;
using BibliotecaViva.DTO;
using BibliotecaViva.DAL.Utils;
using BibliotecaViva.DAL.Interfaces;

namespace BibliotecaViva.DAL
{
    public class TipoRelecaoDAL : BaseDAL, ITipoRelacaoDAL
    {
        public TipoRelecaoDAL(bibliotecavivaContext dataContext) : base(dataContext)
        {
        }
        public void Cadastrar(TipoRelacaoDTO tipoRelacaoDTO)
        {
            if(!ValidarJaCadastrado(tipoRelacaoDTO))
            {
                DataContext.Add(Conversor.Mapear(tipoRelacaoDTO));
                DataContext.SaveChanges();
            }
        }
        public TipoRelacaoDTO Consultar(TipoRelacaoDTO tipoRelacaoDTO)
        {
            var resultado = new Tiporelacao();

            if (string.IsNullOrEmpty(tipoRelacaoDTO.Nome))
                resultado = DataContext.Tiporelacaos.AsNoTracking().FirstOrDefault(tipo => tipo.Codigo == tipoRelacaoDTO.Codigo);
            else
                resultado = DataContext.Tiporelacaos.AsNoTracking().FirstOrDefault(tipo => tipo.Nome.ToLower() == tipoRelacaoDTO.Nome.ToLower());
            
            return Conversor.Mapear(resultado);
        }
        public List<TipoRelacaoDTO> Listar()
        {
            return (from tipo in DataContext.Tiporelacaos 
                orderby
                    tipo.Nome
                select 
                    Conversor.Mapear(tipo)).AsNoTracking().ToList(); 
        }
        private bool ValidarJaCadastrado(TipoRelacaoDTO tipoRelacaoDTO)
        {
            var resultado = DataContext.Tiporelacaos.AsNoTracking().FirstOrDefault(tipoRelacao => tipoRelacao.Nome.ToLower() == tipoRelacaoDTO.Nome.ToLower());
            return resultado != null;
        } 
    }
}