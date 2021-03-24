using System.Data.SQLite.Tools;

namespace BibliotecaViva.DAO
{
    public class Apelido
    {
        [PrimaryKey, AutoIncrement]
        public int? Pessoa { get; set; }
        
        [Unique]
        public string Nome { get; set; }
    }
}