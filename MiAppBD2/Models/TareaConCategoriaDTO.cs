using System;
using System.Collections.Generic;
using System.Text;

namespace MiAppBD2.Models
{
    public class TareaConCategoriaDTO
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string FechaLimite { get; set; }
        public string NombreCategoria { get; set; } // Viene de la tabla Categoria
        public string ColorCategoria { get; set; }
    }

}
