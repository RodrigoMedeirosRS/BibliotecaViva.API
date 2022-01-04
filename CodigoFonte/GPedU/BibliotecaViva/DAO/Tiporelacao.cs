using System;
using System.Collections.Generic;

#nullable disable

namespace BibliotecaViva.DAO
{
    public partial class Tiporelacao
    {
        public Tiporelacao()
        {
            Pessoaregistros = new HashSet<Pessoaregistro>();
        }

        public int Codigo { get; set; }
        public string Nome { get; set; }

        public virtual ICollection<Pessoaregistro> Pessoaregistros { get; set; }
    }
}
