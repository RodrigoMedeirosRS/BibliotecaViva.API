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
            
            foreach(var referencia in registroDTO.Referencias)
                DataContext.Add(new Referencium()
                {
                    Registro = referencia.Registro,
                    Referencia = referencia.Referencia
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
        public List<ReferenciaDTO> ObterReferencia(int codRegistro)
        {
            return (from referencia in DataContext.Referencia
                where 
                    referencia.Registro == codRegistro
                select 
                    new ReferenciaDTO()
                    {
                        Codigo = referencia.Codigo,
                        Registro = (int)referencia.Registro,
                        Referencia = (int)referencia.Referencia
                    }).AsNoTracking().ToList();
        }
        private IQueryable<Referencium> ListarRelacoes(int codRegistro)
        {
            return DataContext.Referencia.Where(relacao => relacao.Registro == codRegistro);
        }
    }
}