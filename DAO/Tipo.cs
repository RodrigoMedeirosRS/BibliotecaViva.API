using System;
using System.Collections.Generic;

#nullable disable

namespace BibliotecaViva.DAO
{
    public partial class Tipo
    {
        public Tipo()
        {
            Registros = new HashSet<Registro>();
        }

        public int Codigo { get; set; }
        public int Tipodeexecucao { get; set; }
        public string Nome { get; set; }
        public string Extensao { get; set; }

        public virtual Tipodeexecucao TipodeexecucaoNavigation { get; set; }
        public virtual ICollection<Registro> Registros { get; set; }
    }
}
