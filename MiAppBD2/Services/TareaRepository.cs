using MiAppBD2.Models;
using SQLite;

namespace MiAppBD2.Services
{
    public class TareaRepository
    {
        private readonly SQLiteConnection _db;

        // Inyectamos el DatabaseService para obtener la conexión
        public TareaRepository(DatabaseService dbService)
        {
            _db = dbService.Connection;
        }

        // --- LECTURA OPTIMIZADA CON SQL CRUDO ---
        public List<Tarea> ObtenerTareas()
        {
            // CONCEPTO DE TEORÍA: Mandamos los NULLs al final de la lista (Slide 12).
            string sql = @"
                SELECT * FROM Tarea 
                ORDER BY 
                    CASE WHEN FechaLimite IS NULL OR FechaLimite = '' THEN 1 ELSE 0 END, 
                    FechaLimite ASC;";

            return _db.Query<Tarea>(sql);
        }

        // --- TRANSACCIÓN SEGURA DE GUARDADO ---
        public void GuardarTarea(Tarea tarea)
        {
            // CONCEPTO DE TEORÍA: Transacciones para proteger la flash memory (Slide 6 y 7)
            _db.RunInTransaction(() =>
            {
                if (tarea.Id != 0)
                    _db.Update(tarea); // Si tiene ID, actualiza
                else
                    _db.Insert(tarea); // Si no, inserta 
            });

            // El codigo anterior es lo mismo que el siguiente, pero simplifica la gestion de errores y asegura que la transacción se maneje correctamente.
            //try
            //{
            //    _db.BeginTransaction();
            //    if (tarea.Id != 0)
            //        _db.Update(tarea); 
            //    else
            //        _db.Insert(tarea); 
            //    _db.Commit();
            //}
            //catch (Exception)
            //{
            //    _db.Rollback();
            //    throw;
            //}
        }

        public void EliminarTarea(Tarea tarea)
        {
            _db.Delete(tarea);
        }

        public List<TareaConCategoriaDTO> ObtenerTareasConSuCategoria()
        {
            // TEORÍA: Ejecutamos un JOIN localmente en el celular.
            // Usamos LEFT JOIN para que las tareas viejas de la Versión 1 (cuyo CategoriaId es 0 o nulo) 
            // no desaparezcan de la pantalla. Si usamos INNER JOIN, se ocultarían.
            string sql = @"
                SELECT 
                    t.Id, 
                    t.Titulo, 
                    t.FechaLimite,
                    IFNULL(c.Nombre, 'Sin Categoría') AS NombreCategoria,
                    IFNULL(c.ColorHex, '#CCCCCC') AS ColorCategoria   -- Extraemos el color (Gris por defecto)
                FROM Tarea t
                LEFT JOIN Categoria c ON t.CategoriaId = c.Id
                ORDER BY 
                    CASE WHEN t.FechaLimite IS NULL OR t.FechaLimite = '' THEN 1 ELSE 0 END, 
                    t.FechaLimite ASC;";

            return _db.Query<TareaConCategoriaDTO>(sql);
        }

        public Tarea ObtenerTareaPorId(int id)
        {
            // Encuentra un registro por su Primary Key
            return _db.Find<Tarea>(id);
        }
    }



}

