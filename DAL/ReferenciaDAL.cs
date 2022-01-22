using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

using BibliotecaViva.DAO;
using BibliotecaViva.DTO;
using BibliotecaViva.DAL.Utils;
using BibliotecaViva.DAL.Interfaces;

namespace BibliotecaViva.DAL
{
    public class ReferenciaDAL : BaseDAL, IReferenciaDAL
    {
        public ReferenciaDAL(bibliotecavivaContext dataContext) : base(dataContext)
        {
        }
        public void VincularReferencia(RegistroDTO registroDTO)
        {          
            var relacoes = ListarRelacoes((int)registroDTO.Codigo);

            foreach (var ralacao in relacoes)
                DataContext.Referencia.Remove(ralacao);  
            
            foreach(var relacao in registroDTO.Referencias)
                DataContext.Add(new Referencium()
                {
                    Registro = (int)relacao.RegistroPessoaID,
                    Referencia = (int)relacao.RelacaoID,
                });
        }
        public List<RegistroDTO> ObterReferenciaCompleta(RegistroDTO registroDTO, IRegistroDAL registroDAL)
        {
            var referencias = ListarRelacoes((int)registroDTO.Codigo);
            var registros = new List<RegistroDTO>();

            if (referencias == null)
                return registros;

            foreach(var referencia in referencias)
                registros.Add(registroDAL.Consultar((int)referencia.Referencia));

            return registros;
        }
        public List<RelacaoDTO> ObterReferencia(int codRegistro)
        {
            return (from relacao in DataContext.Referencia
                where 
                    relacao.Registro == codRegistro
                select 
                    new RelacaoDTO()
                    {
                        Codigo = relacao.Codigo,
                        RegistroPessoaID = (int)relacao.Registro,
                        RelacaoID = (int)relacao.Referencia
                    }).AsNoTracking().ToList();
        }
        private IQueryable<Referencium> ListarRelacoes(int codRegistro)
        {
            return DataContext.Referencia.Where(relacao => relacao.Registro == codRegistro);
        }
    }
}