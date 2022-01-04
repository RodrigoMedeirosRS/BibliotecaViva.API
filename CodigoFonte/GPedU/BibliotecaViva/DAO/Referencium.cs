using System;
using System.Collections.Generic;

#nullable disable

namespace BibliotecaViva.DAO
{
    public partial class Referencium
    {
        public int Codigo { get; set; }
        public int Referencia { get; set; }
        public int Registro { get; set; }

        public virtual Registro ReferenciaNavigation { get; set; }
        public virtual Registro RegistroNavigation { get; set; }
    }
}
