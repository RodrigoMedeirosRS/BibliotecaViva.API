﻿using System.Data.SQLite.Tools;

namespace BibliotecaViva.DTO.Model
{
    public class TipoEvento
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Nome { get; set; }
    }
}
