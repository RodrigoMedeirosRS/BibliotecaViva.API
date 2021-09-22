using System.Linq;
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
            apelidoDTO.Codigo = ValidarJaCadastrado(apelidoDTO);
            if (apelidoDTO.Codigo != null)
                DataContext.ObterDataContext().InsertOrReplace(new Pessoaapelido()
                {
                    Pessoa = (int)pessoaDTO.Codigo,
                    Apelido = (int)apelidoDTO.Codigo
                });
        }

        public void VincularRegistro(ApelidoDTO apelidoDTO, RegistroDTO registroDTO)
        {
            apelidoDTO.Codigo = ValidarJaCadastrado(apelidoDTO);
            if (apelidoDTO.Codigo != null)
                DataContext.ObterDataContext().InsertOrReplace(new Registroapelido()
                {
                    Registro = (int)registroDTO.Codigo,
                    Apelido = (int)apelidoDTO.Codigo
                });
        }
        
        public void RemoverVinculo(int? codigoPessoa)
        {
            var resultado = DataContext.ObterDataContext().Table<Pessoaapelido>().FirstOrDefault(apelido => apelido.Pessoa == codigoPessoa);
            if (resultado != null)
                DataContext.ObterDataContext().Delete(resultado);
        }

        public void RemoverVinculoRegistro(int? codigoRegistro)
        {
            var resultado = DataContext.ObterDataContext().Table<Registroapelido>().FirstOrDefault(apelido => apelido.Registro == codigoRegistro);
            if (resultado != null)
                DataContext.ObterDataContext().Delete(resultado);
        }

        private bool ValidarJaCadastrado(ApelidoDTO apelidoDTO)
        {
            var resultado = DataContext.Apelidos.FirstOrDefault(apelido => apelido.Nome == apelidoDTO.Nome);
            return resultado != null;
        }  
    }
}