using System.Linq;
using Microsoft.EntityFrameworkCore;

using BibliotecaViva.DAO;
using BibliotecaViva.DTO;
using BibliotecaViva.DAL.Utils;
using BibliotecaViva.DAL.Interfaces;

namespace BibliotecaViva.DAL
{
    public class DescricaoDAL : BaseDAL, IDescricaoDAL
    {
        public DescricaoDAL(bibliotecavivaContext dataContext) : base(dataContext)
        {
        }
        public void Cadastrar(DescricaoDTO descricaoDTO)
        {
            var descricao = ValidarJaCadastrado(descricaoDTO);
            if (descricao != null)
            {
                descricao.Conteudo = descricaoDTO.Conteudo;
                DataContext.Update(descricao);
                DataContext.SaveChanges();
            }
            else
            {
                DataContext.Add(Conversor.Mapear(descricaoDTO));
                DataContext.SaveChanges();
            }
        }
        public void Remover(int? codigoRegistro)
        {
            var apelido = DataContext.Descricaos.AsNoTracking().FirstOrDefault(descricao => descricao.Registro == codigoRegistro);
            DataContext.Remove(apelido);
            DataContext.SaveChanges();
        }
        private Descricao ValidarJaCadastrado(DescricaoDTO descricaoDTO)
        {
            return DataContext.Descricaos.AsNoTracking().FirstOrDefault(descricao => descricao.Registro == descricaoDTO.Registro);
        }  
    }
}