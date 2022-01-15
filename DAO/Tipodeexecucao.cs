using System;
using System.Collections.Generic;

#nullable disable

namespace BibliotecaViva.DAO
{
    public partial class Tipodeexecucao
    {
        public Tipodeexecucao()
        {
            Tipos = new HashSet<Tipo>();
        }

        public int Codigo { get; set; }
        public int Nome { get; set; }

        public virtual ICollection<Tipo> Tipos { get; set; }
    }
}
