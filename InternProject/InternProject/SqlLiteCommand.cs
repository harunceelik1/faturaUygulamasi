using System.Data.SQLite;

namespace InternProject
{
    internal class SqlLiteCommand
    {
        internal object Parameters;
        private string sorgu;
        private SQLiteConnection connection;

        public SqlLiteCommand(string sorgu, SQLiteConnection connection)
        {
            this.sorgu = sorgu;
            this.connection = connection;
        }

        public string CommandText { get; internal set; }

        internal void ExecuteNonQuery()
        {
            throw new NotImplementedException();
        }

        internal SQLiteDataReader ExecuteReader()
        {
            throw new NotImplementedException();
        }
    }
}