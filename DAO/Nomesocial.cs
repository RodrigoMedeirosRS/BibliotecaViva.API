using System;
using System.Collections.Generic;

#nullable disable

namespace BibliotecaViva.DAO
{
    public partial class Nomesocial
    {
        public int Codigo { get; set; }
        public int Pessoa { get; set; }
        public string Nome { get; set; }

        public virtual Pessoa PessoaNavigation { get; set; }
    }
}
