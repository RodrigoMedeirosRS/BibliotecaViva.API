using System;
using System.Collections.Generic;

#nullable disable

namespace BibliotecaViva.DAO
{
    public partial class Pessoalocalizacao
    {
        public int Codigo { get; set; }
        public int Localizacaogeografica { get; set; }
        public int Pessoa { get; set; }

        public virtual Localizacaogeografica LocalizacaogeograficaNavigation { get; set; }
        public virtual Pessoa PessoaNavigation { get; set; }
    }
}
