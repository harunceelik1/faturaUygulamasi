using System.Data;
using System.Data.SQLite;

namespace InternProject
{
    internal class SQLiteDataadapter
    {
        private string sql;
        private SQLiteConnection connection;
        

        public SQLiteDataadapter(string v)
        {
        }

        public SQLiteDataadapter(string sql, SQLiteConnection connection)
        {
            this.sql = sql;
            this.connection = connection;
        }

       

        internal void Fill(DataTable dt)
        {
            throw new NotImplementedException();
        }

        internal void Fill(DataSet ds, string v)
        {
            
        }
    }
}