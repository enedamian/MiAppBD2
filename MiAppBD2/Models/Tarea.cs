using SQLite;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiAppBD2.Models
{
    public class Tarea
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [MaxLength(100), NotNull]
        public string Titulo { get; set; }

        public string Estado { get; set; } = "pendiente";
        public string? FechaLimite { get; set; }

        //Campo FK para la parte 2
        [Indexed, ForeignKey(nameof(Categoria))]
        public int CategoriaId { get; set; }
    }
}
