using System.Linq;
using Microsoft.EntityFrameworkCore;

using BibliotecaViva.DAO;
using BibliotecaViva.DTO;
using BibliotecaViva.DAL.Utils;
using BibliotecaViva.DAL.Interfaces;

namespace BibliotecaViva.DAL 
{
    public class ApelidoDAL : BaseDAL, IApelidoDAL
    {
        public ApelidoDAL(bibliotecavivaContext dataContext) : base(dataContext)
        {

        }

        public void Cadastrar(ApelidoDTO apelidoDTO)
        {
            if (!ValidarJaCadastrado(apelidoDTO))
            {
                DataContext.Add(Conversor.Mapear(apelidoDTO));
                DataContext.SaveChanges();
            }
        }

        public void VincularPessoa(ApelidoDTO apelidoDTO, PessoaDTO pessoaDTO)
        {
            DataContext.Add(new Pessoaapelido()
            {
                Pessoa = (int)pessoaDTO.Codigo,
                Apelido = (int)apelidoDTO.Codigo
            });
            DataContext.SaveChanges();
        }

        public void VincularRegistro(ApelidoDTO apelidoDTO, RegistroDTO registroDTO)
        {
            DataContext.Add(new Registroapelido()
            {
                Registro = (int)registroDTO.Codigo,
                Apelido = (int)apelidoDTO.Codigo
            });
            DataContext.SaveChanges();
        }
        
        public void RemoverVinculo(int? codigoPessoa)
        {
            var apelido = DataContext.Pessoaapelidos.AsNoTracking().FirstOrDefault(apelido => apelido.Pessoa == codigoPessoa);
            DataContext.Remove(apelido);
            DataContext.SaveChanges();
        }

        public void RemoverVinculoRegistro(int? codigoRegistro)
        {
            var apelido = DataContext.Registroapelidos.AsNoTracking().FirstOrDefault(apelido => apelido.Registro == codigoRegistro);
            DataContext.Remove(apelido);
            DataContext.SaveChanges();
        }

        private bool ValidarJaCadastrado(ApelidoDTO apelidoDTO)
        {
            var resultado = DataContext.Apelidos.AsNoTracking().FirstOrDefault(apelido => apelido.Nome == apelidoDTO.Nome);
            return resultado != null;
        }  
    }
}