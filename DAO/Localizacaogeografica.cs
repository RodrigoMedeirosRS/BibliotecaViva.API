using System;
using System.Collections.Generic;

#nullable disable

namespace BibliotecaViva.DAO
{
    public partial class Localizacaogeografica
    {
        public Localizacaogeografica()
        {
            Registrolocalizacaos = new HashSet<Registrolocalizacao>();
        }

        public int Codigo { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public virtual ICollection<Registrolocalizacao> Registrolocalizacaos { get; set; }
    }
}
