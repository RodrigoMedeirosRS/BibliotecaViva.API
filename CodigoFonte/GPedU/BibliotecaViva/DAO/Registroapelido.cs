using System;
using System.Collections.Generic;

#nullable disable

namespace BibliotecaViva.DAO
{
    public partial class Registroapelido
    {
        public int Codigo { get; set; }
        public int Registro { get; set; }
        public int Apelido { get; set; }

        public virtual Apelido ApelidoNavigation { get; set; }
        public virtual Registro RegistroNavigation { get; set; }
    }
}
