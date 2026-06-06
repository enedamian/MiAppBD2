// importamos la carpeta de modelos para usar la clase Tarea
using MiAppBD2.Models;
using SQLite;

namespace MiAppBD2.Services
{
    public class DatabaseService
    {
        // Version simplificada - síncrona

        public SQLiteConnection Connection;

        public DatabaseService()
        {
            // 1. definimos la ruta en el almacenamiento privado (sandbox) de la app
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "tareas.db");

            // 2. creamos la conexión a la base de datos
            Connection = new SQLiteConnection(dbPath);

            // Activamos WAL para mejor concurrencia
            Connection.ExecuteScalar<string>("PRAGMA journal_mode=WAL;");

            //Parte 2: Activamos las claves foráneas para asegurar la integridad referencial
            Connection.Execute("PRAGMA foreign_keys = ON;");

            // 3. creamos la tabla si no existe
            Connection.CreateTable<Tarea>();
            Connection.CreateTable<Categoria>(); // <- Creamos la nueva tabla

            // Generar datos semilla (Solo si la tabla de categorías está vacía)
            if (Connection.Table<Categoria>().Count() == 0)
            {
                Connection.Insert(new Categoria { Nombre = "Trabajo", ColorHex = "#FF0000" });
                Connection.Insert(new Categoria { Nombre = "Hogar", ColorHex = "#00FF00" });
                Connection.Insert(new Categoria { Nombre = "Estudio", ColorHex = "#0000FF" });
            }

        }


    }
}