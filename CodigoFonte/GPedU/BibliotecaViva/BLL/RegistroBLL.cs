using BibliotecaViva.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;
using BibliotecaViva.DTO.Dominio;
using BibliotecaViva.DAL.Interfaces;
using BibliotecaViva.BLL.Interfaces;

namespace BibliotecaViva.BLL
{
    public class RegistroBLL : BaseBLL, IRegistroBLL
    {
        private IRegistroDAL RegistroDAL { get; set; }
        private IReferenciaDAL ReferenciaDAL { get; set; }
        public RegistroBLL(IRegistroDAL registroDAL, IReferenciaDAL referenciaDAL)
        {
            RegistroDAL = registroDAL;
            ReferenciaDAL = referenciaDAL;
        }
        public async Task<string> Cadastrar(RegistroDTO registro) 
        {
            RegistroDAL.Cadastrar(registro);
            return registro.Nome + " Registrado(a) com Sucesso!";
        }
        public async Task<List<RegistroDTO>> Consultar(RegistroConsulta registro)
        {
            return RegistroDAL.Consultar(new RegistroDTO()
            {
                Nome = registro.Nome,
                Idioma = registro.Idioma
            });
        }
        public async Task<List<RegistroDTO>> ObterReferencias(int codRegistro)
        {
            return ReferenciaDAL.ObterReferenciaCompleta(new RegistroDTO()
            {
                Codigo = codRegistro
            }, RegistroDAL);
        }
    }
}
