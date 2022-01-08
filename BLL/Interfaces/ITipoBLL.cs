using System.Collections.Generic;
using System.Threading.Tasks;
using BibliotecaViva.DTO;

namespace BibliotecaViva.BLL.Interfaces
{
    public interface ITipoBLL
    {
        Task<string> Cadastrar(IdiomaDTO idiomaDTO);
        Task<string> Cadastrar(TipoDTO tipoDTO);
        Task<string> Cadastrar(TipoRelacaoDTO tipoRelacaoDTO);
        Task<List<IdiomaDTO>> ConsultarIdiomas();
        Task<List<TipoDTO>> ConsultarTipos();
        Task<List<TipoRelacaoDTO>> ConsultarTiposRelacao();
    }
}