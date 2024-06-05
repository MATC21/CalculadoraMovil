using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tarea1.Models
{
    public class Operaciones
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        public string operacion { get; set; }
        public double numero1 { get; set; }
        public double numero2 { get; set; }
        public double total { get; set; }
        public DateTime fechaRegistro { get; set; }
    }
}
