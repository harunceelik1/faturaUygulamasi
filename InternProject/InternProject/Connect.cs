using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternProject
{
    public class Connect
    {
        public static SQLiteConnection connection = new SQLiteConnection("Data source=E:\\Udemy C#\\Deneme1\\Deneme1\\bin\\Debug\\upload_system.db;Version=3;");
        public SQLiteConnection Connection()
        {


            SQLiteConnection connection = new SQLiteConnection("Data source=upload_system.db;Version=3;");
            connection.Open();
            return connection;

        }
    }
}