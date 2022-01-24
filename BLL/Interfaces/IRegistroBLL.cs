using System.Threading.Tasks;
using System.Collections.Generic;
using BibliotecaViva.DTO;
using BibliotecaViva.DTO.Dominio;

namespace BibliotecaViva.BLL.Interfaces
{
    public interface IRegistroBLL
    {
        Task<string> Cadastrar(RegistroDTO registro);
        Task<List<RegistroDTO>> Consultar(RegistroConsulta registro);
        Task<ReferenciaRetorno> ObterReferencias(int codRegistro);
    }
}
