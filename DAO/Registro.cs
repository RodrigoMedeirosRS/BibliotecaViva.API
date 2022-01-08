using System;
using System.Collections.Generic;

#nullable disable

namespace BibliotecaViva.DAO
{
    public partial class Registro
    {
        public Registro()
        {
            Descricaos = new HashSet<Descricao>();
            Pessoaregistros = new HashSet<Pessoaregistro>();
            ReferenciumReferenciaNavigations = new HashSet<Referencium>();
            ReferenciumRegistroNavigations = new HashSet<Referencium>();
            Registroapelidos = new HashSet<Registroapelido>();
            Registrolocalizacaos = new HashSet<Registrolocalizacao>();
        }

        public int Codigo { get; set; }
        public int Idioma { get; set; }
        public int Tipo { get; set; }
        public string Nome { get; set; }
        public string Conteudo { get; set; }
        public DateTime Datainsercao { get; set; }

        public virtual Idioma IdiomaNavigation { get; set; }
        public virtual Tipo TipoNavigation { get; set; }
        public virtual ICollection<Descricao> Descricaos { get; set; }
        public virtual ICollection<Pessoaregistro> Pessoaregistros { get; set; }
        public virtual ICollection<Referencium> ReferenciumReferenciaNavigations { get; set; }
        public virtual ICollection<Referencium> ReferenciumRegistroNavigations { get; set; }
        public virtual ICollection<Registroapelido> Registroapelidos { get; set; }
        public virtual ICollection<Registrolocalizacao> Registrolocalizacaos { get; set; }
    }
}
