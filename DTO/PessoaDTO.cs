using System.Collections.Generic;

namespace BibliotecaViva.DTO
{
    public class PessoaDTO : BaseDTO
    {
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Genero { get; set; }
        public string Apelido { get; set; }
        public string NomeSocial { get; set; }
        public long? Latitude { get; set; }
        public long? Longitude { get; set; }
        public List<PessoaRegistroDTO> Relacoes { get; set; }
    }
}