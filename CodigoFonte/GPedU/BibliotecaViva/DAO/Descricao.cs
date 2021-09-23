using System;
using System.Collections.Generic;

#nullable disable

namespace BibliotecaViva.DAO
{
    public partial class Descricao
    {
        public int Codigo { get; set; }
        public int Registro { get; set; }
        public string Conteudo { get; set; }

        public virtual Registro RegistroNavigation { get; set; }
    }
}
