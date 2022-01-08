using System;
using System.Collections.Generic;

#nullable disable

namespace BibliotecaViva.DAO
{
    public partial class Registrolocalizacao
    {
        public int Codigo { get; set; }
        public int Registro { get; set; }
        public int Localizacaogeografica { get; set; }

        public virtual Localizacaogeografica LocalizacaogeograficaNavigation { get; set; }
        public virtual Registro RegistroNavigation { get; set; }
    }
}
