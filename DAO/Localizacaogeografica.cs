using System;
using System.Collections.Generic;

#nullable disable

namespace BibliotecaViva.DAO
{
    public partial class Localizacaogeografica
    {
        public Localizacaogeografica()
        {
            Pessoalocalizacaos = new HashSet<Pessoalocalizacao>();
            Registrolocalizacaos = new HashSet<Registrolocalizacao>();
        }

        public int Codigo { get; set; }
        public long Latitude { get; set; }
        public long Longitude { get; set; }

        public virtual ICollection<Pessoalocalizacao> Pessoalocalizacaos { get; set; }
        public virtual ICollection<Registrolocalizacao> Registrolocalizacaos { get; set; }
    }
}
