using System;
using System.Collections.Generic;

#nullable disable

namespace BibliotecaViva.DAO
{
    public partial class Pessoaregistro
    {
        public int Codigo { get; set; }
        public int Tiporelacao { get; set; }
        public int Registro { get; set; }
        public int Pessoa { get; set; }

        public virtual Pessoa PessoaNavigation { get; set; }
        public virtual Registro RegistroNavigation { get; set; }
        public virtual Tiporelacao TiporelacaoNavigation { get; set; }
    }
}
