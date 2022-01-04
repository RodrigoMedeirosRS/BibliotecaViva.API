using System;
using System.Collections.Generic;

#nullable disable

namespace BibliotecaViva.DAO
{
    public partial class Apelido
    {
        public Apelido()
        {
            Pessoaapelidos = new HashSet<Pessoaapelido>();
            Registroapelidos = new HashSet<Registroapelido>();
        }

        public int Codigo { get; set; }
        public string Nome { get; set; }

        public virtual ICollection<Pessoaapelido> Pessoaapelidos { get; set; }
        public virtual ICollection<Registroapelido> Registroapelidos { get; set; }
    }
}
