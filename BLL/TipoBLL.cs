using BibliotecaViva.DTO;
using System.Threading.Tasks;
using System.Collections.Generic;
using BibliotecaViva.DAL.Interfaces;
using BibliotecaViva.BLL.Interfaces;

namespace BibliotecaViva.BLL
{
    public class TipoBLL : BaseBLL, ITipoBLL
    {
        private ITipoDAL TipoDAL { get; set; }
        private IIdiomaDAL IdiomaDAL { get; set; }
        private ITipoRelacaoDAL TipoRelacaoDAL { get; set; }
        public TipoBLL(ITipoDAL tipoDAL, IIdiomaDAL idiomaDAL, ITipoRelacaoDAL tipoRelacaoDAL)
        {
            TipoDAL = tipoDAL;
            IdiomaDAL = idiomaDAL;
            TipoRelacaoDAL = tipoRelacaoDAL;
        }
        public async Task<string> Cadastrar(IdiomaDTO idiomaDTO)
        {
            IdiomaDAL.Cadastrar(idiomaDTO);
            return "Idioma " + idiomaDTO.Nome + "Cadastrado com Sucesso!";
        }
        public async Task<string> Cadastrar(TipoDTO tipoDTO)
        {
            TipoDAL.Cadastrar(tipoDTO);
            return "Idioma " + tipoDTO.Nome + "Cadastrado com Sucesso!";
        }
        public async Task<string> Cadastrar(TipoRelacaoDTO tipoRelacaoDTO)
        {
            TipoRelacaoDAL.Cadastrar(tipoRelacaoDTO);
            return "Idioma " + tipoRelacaoDTO.Nome + "Cadastrado com Sucesso!";
        }
        public async Task<List<IdiomaDTO>> ConsultarIdiomas()
        {
            return IdiomaDAL.Listar();
        }
        public async Task<List<TipoDTO>> ConsultarTipos()
        {
            return TipoDAL.Listar();
        }
        public async Task<List<TipoRelacaoDTO>> ConsultarTiposRelacao()
        {
            return TipoRelacaoDAL.Listar();
        }
    }
}