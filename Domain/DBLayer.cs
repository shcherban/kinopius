using System.Collections.Generic;
using System.Data.Common;

namespace Domain
{
    public class DBLayer
    {
        private DbConnection _connection;
        public DBLayer(DbConnection connection)
        {
            _connection = connection;
        }

        public void SaveFilm(Film film)
        {
            
        }

        public Film GetFilm(int id)
        {
            Film res = new Film();
            return res;
        }

        public List<Film> SearchFilm(string searchString)
        {
            List<Film> res = new List<Film>();
            return res;
        }
    }
}