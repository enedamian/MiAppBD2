using SQLite;

namespace MiAppBD2.Models
{
    public class Categoria
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }

        [MaxLength(50)]
        public string Nombre { get; set; }
        public string ColorHex { get; set; }
    }
}
