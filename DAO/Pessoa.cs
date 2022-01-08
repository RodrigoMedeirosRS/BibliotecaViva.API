using System;
using System.Collections.Generic;

#nullable disable

namespace BibliotecaViva.DAO
{
    public partial class Pessoa
    {
        public Pessoa()
        {
            Nomesocials = new HashSet<Nomesocial>();
            Pessoaapelidos = new HashSet<Pessoaapelido>();
            Pessoalocalizacaos = new HashSet<Pessoalocalizacao>();
            Pessoaregistros = new HashSet<Pessoaregistro>();
        }

        public int Codigo { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Genero { get; set; }

        public virtual ICollection<Nomesocial> Nomesocials { get; set; }
        public virtual ICollection<Pessoaapelido> Pessoaapelidos { get; set; }
        public virtual ICollection<Pessoalocalizacao> Pessoalocalizacaos { get; set; }
        public virtual ICollection<Pessoaregistro> Pessoaregistros { get; set; }
    }
}
