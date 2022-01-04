using System;
using System.Collections.Generic;

#nullable disable

namespace BibliotecaViva.DAO
{
    public partial class Pessoaapelido
    {
        public int Codigo { get; set; }
        public int Apelido { get; set; }
        public int Pessoa { get; set; }

        public virtual Apelido ApelidoNavigation { get; set; }
        public virtual Pessoa PessoaNavigation { get; set; }
    }
}
