using System.Data;
using System.Data.SQLite;

namespace InternProject
{
    internal class SqlLiteDataAdapter
    {
        private string v;
        private SqlLiteConnection baglanti;
        private string sorgu;
        private SQLiteConnection connection;

        public SqlLiteDataAdapter(string v, SqlLiteConnection baglanti)
        {
            this.v = v;
            this.baglanti = baglanti;
        }

        public SqlLiteDataAdapter(string sorgu, SQLiteConnection connection)
        {
            this.sorgu = sorgu;
            this.connection = connection;
        }

        internal void Fill(DataTable tablo)
        {
            
        }

        public static implicit operator SqlLiteDataAdapter(SQLiteDataadapter v)
        {
            throw new NotImplementedException();
        }

        internal SqlLiteDataReader ExecuteReader()
        {
            throw new NotImplementedException();
        }

        internal void Fill(DataSet ds, string v)
        {
            throw new NotImplementedException();
        }
    }
}