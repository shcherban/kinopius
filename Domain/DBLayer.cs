using System.Collections.Generic;
using System.Data.Common;
using Microsoft.Data.Sqlite;

namespace Domain
{
    public class DBLayer
    {
        private DbConnection _connection;
        public DBLayer(DbConnection connection)
        {
            string connectionString = "Data Source=usersdata.db";
            _connection = new SqliteConnection(connectionString);
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