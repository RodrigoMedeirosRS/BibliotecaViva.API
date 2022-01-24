using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

using BibliotecaViva.DAO;
using BibliotecaViva.DTO;
using BibliotecaViva.DAL.Utils;
using BibliotecaViva.DTO.Dominio;
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
            var relacoes = ListarReferencias((int)registroDTO.Codigo);

            foreach (var ralacao in relacoes)
                DataContext.Referencia.Remove(ralacao);  
            
            foreach(var relacao in registroDTO.Referencias)
                DataContext.Add(new Referencium()
                {
                    Registro = registroDTO.Codigo,
                    Referencia = (int)relacao.RelacaoID,
                });
            DataContext.SaveChanges();
        }
        public List<RegistroDTO> ObterReferenciaCompleta(RegistroDTO registroDTO, IRegistroDAL registroDAL)
        {
            var referencias = ListarReferencias((int)registroDTO.Codigo);
            var registros = new List<RegistroDTO>();

            if (referencias == null)
                return registros;

            foreach(var referencia in referencias)
                registros.Add(registroDAL.Consultar((int)referencia.Referencia));

            return registros;
        }
        public List<PessoaDTO> ObterPessoasReferenciadas(RegistroDTO registroDTO, IPessoaDAL pessoaDAL)
        {
            var referencias = ListarRelacoes((int)registroDTO.Codigo);
            var pessoas = new List<PessoaDTO>();

            if (referencias == null)
                return pessoas;
            foreach(var referencia in referencias)
                pessoas.Add(pessoaDAL.Consultar((int)referencia.Pessoa));
            
            return pessoas;            
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
        private List<Referencium> ListarReferencias(int codRegistro)
        {
            return (from referencia in DataContext.Referencia.AsNoTracking()
                where
                    referencia.Registro == codRegistro
                select referencia).AsNoTracking().ToList();
        }
        private List<Pessoaregistro> ListarRelacoes(int codRegistro)
        {
            return(from referencia in DataContext.Pessoaregistros.AsNoTracking()
                where  
                    referencia.Registro == codRegistro
                select referencia).AsNoTracking().ToList();
        }
    }
}