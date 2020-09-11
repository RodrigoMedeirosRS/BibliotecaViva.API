﻿using System.Data.SQLite.Tools;

namespace BibliotecaViva.DTO.Model
{
    public class DossieDocumento
    {
        [PrimaryKey, Indexed]
        public int Dossie { get; set; }
        
        [Indexed]
        public int Documento { get; set; }
    }
}