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
                Pessoa = pessoaDTO.Codigo,
                Apelido = apelidoDTO.Codigo
            });
            DataContext.SaveChanges();
        }
        public void VincularRegistro(ApelidoDTO apelidoDTO, RegistroDTO registroDTO)
        {
            DataContext.Add(new Registroapelido()
            {
                Registro = registroDTO.Codigo,
                Apelido = apelidoDTO.Codigo
            });
            DataContext.SaveChanges();
        }
        public void RemoverVinculo(int codigoPessoa)
        {
            var apelido = DataContext.Pessoaapelidos.AsNoTracking().FirstOrDefault(apelido => apelido.Pessoa == codigoPessoa);
            if (apelido != null)
                DataContext.Remove(apelido);
            DataContext.SaveChanges();
        }
        public void RemoverVinculoRegistro(int codigoRegistro)
        {
            var registro = DataContext.Registroapelidos.AsNoTracking().FirstOrDefault(apelido => apelido.Registro == codigoRegistro);
            if (registro != null)
                DataContext.Remove(registro);
            DataContext.SaveChanges();
        }
        private bool ValidarJaCadastrado(ApelidoDTO apelidoDTO)
        {
            var resultado = DataContext.Apelidos.AsNoTracking().FirstOrDefault(apelido => apelido.Nome.ToLower() == apelidoDTO.Nome.ToLower());
            return resultado != null;
        }  
    }
}