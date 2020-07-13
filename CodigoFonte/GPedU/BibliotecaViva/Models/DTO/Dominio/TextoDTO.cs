using System;
namespace BibliotecaViva.Models.DTO.Dominio
{
    public class TextoDTO : DocumentoDTO
    {
        public TextoDTO (int id, string nome, string texto, DateTime dataRegistro, DateTime dataDataDigtalizacao) : base(id, nome, dataRegistro, dataDataDigtalizacao)
        {
            Texto = texto;
        }
        public TextoDTO (string nome, string texto, DateTime dataRegistro, DateTime dataDataDigtalizacao) : base(nome, dataRegistro, dataDataDigtalizacao)
        {
            Texto = texto;
        }
        public string Texto { get; set;}
    }
}