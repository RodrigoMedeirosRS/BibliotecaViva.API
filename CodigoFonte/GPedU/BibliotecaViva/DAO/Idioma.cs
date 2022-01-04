using System;
using System.Collections.Generic;

#nullable disable

namespace BibliotecaViva.DAO
{
    public partial class Idioma
    {
        public Idioma()
        {
            Registros = new HashSet<Registro>();
        }

        public int Codigo { get; set; }
        public string Nome { get; set; }

        public virtual ICollection<Registro> Registros { get; set; }
    }
}
