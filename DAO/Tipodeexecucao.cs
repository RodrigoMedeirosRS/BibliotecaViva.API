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
        public string Nome { get; set; }
        public bool Binario { get; set; }

        public virtual ICollection<Tipo> Tipos { get; set; }
    }
}
