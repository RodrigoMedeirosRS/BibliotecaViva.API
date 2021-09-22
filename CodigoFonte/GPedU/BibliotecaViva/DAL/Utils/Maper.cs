using BibliotecaViva.DAO;
using BibliotecaViva.DTO;

namespace BibliotecaViva.DAL.Utils
{
    internal static class Conversor
    {
        internal static Apelido Mapear(ApelidoDTO apelido)
        {
            return new Apelido()
            {
                Codigo = (int)apelido.Codigo,
                Nome = apelido.Nome
            };
        }
        internal static ApelidoDTO Mapear(Apelido apelido)
        {
            return new ApelidoDTO()
            {
                Codigo = apelido.Codigo,
                Nome = apelido.Nome
            };
        }
    }
}