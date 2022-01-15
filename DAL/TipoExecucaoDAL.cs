using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

using BibliotecaViva.DAO;
using BibliotecaViva.DTO;
using BibliotecaViva.DAL.Utils;
using BibliotecaViva.DAL.Interfaces;

namespace BibliotecaViva.DAL
{
    public class TipoExecucaoDAL : BaseDAL, ITipoExecucaoDAL
    {
        public TipoExecucaoDAL(bibliotecavivaContext dataContext) : base(dataContext)
        {

        }
        public List<TipoExecucaoDTO> Listar()
        {
            return (from tipo in DataContext.Tipodeexecucaos
                orderby
                    tipo.Codigo
                select
                    Conversor.Mapear(tipo)).ToList();
        }
    }
}