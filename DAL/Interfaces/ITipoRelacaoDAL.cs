using System.Collections.Generic;
using BibliotecaViva.DTO;

namespace BibliotecaViva.DAL.Interfaces
{
    public interface ITipoRelacaoDAL
    {
        void Cadastrar(TipoRelacaoDTO tipoRelacaoDTO);
        TipoRelacaoDTO Consultar(TipoRelacaoDTO tipoRelacaoDTO);
        List<TipoRelacaoDTO> Listar();
    }
}