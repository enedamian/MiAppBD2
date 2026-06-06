using MiAppBD2.Models;

namespace MiAppBD2.Services
{
    public class CategoriaRepository
    {
        private readonly SQLite.SQLiteConnection _db;

        public CategoriaRepository(DatabaseService dbService)
        {
            _db = dbService.Connection;
        }

        public List<Categoria> ObtenerCategorias()
        {
            return _db.Table<Categoria>().ToList();
        }



    }

}
