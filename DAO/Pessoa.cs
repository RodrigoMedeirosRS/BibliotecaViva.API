using System;
using System.Collections.Generic;

#nullable disable

namespace BibliotecaViva.DAO
{
    public partial class Pessoa
    {
        public Pessoa()
        {
            Pessoaapelidos = new HashSet<Pessoaapelido>();
            Pessoaregistros = new HashSet<Pessoaregistro>();
        }

        public int Codigo { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Genero { get; set; }

        public virtual ICollection<Pessoaapelido> Pessoaapelidos { get; set; }
        public virtual ICollection<Pessoaregistro> Pessoaregistros { get; set; }
    }
}
