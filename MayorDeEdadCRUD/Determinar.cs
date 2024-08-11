using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MayorDeEdadCRUD
{
    [Table("determinar")]

    public class Determinar
    {
        [PrimaryKey]
        [AutoIncrement]
        [Column("id")]
        public int Id { get; set; }
        [Column("nombre")]
        public string? Nombre { get; set; }
        [Column("edad")]
        public string? Edad { get; set;}
        [Column("ley")]
        public string? Ley { get; set; }
    }
}
