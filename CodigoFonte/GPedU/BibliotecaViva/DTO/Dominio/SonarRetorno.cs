using System.Collections.Generic;

namespace BibliotecaViva.DTO.Dominio
{
    public class SonarRetorno
    {
        public List<RegistroDTO> Registros { get; set; }
        public List<PessoaDTO> Pessoas { get; set; }
    }
}