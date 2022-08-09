using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace InternProject
{
    
    public class CRUD
    {
        public static DataTable dt;
        
        public static DataTable Listele(string sql)
        {
            Connect con = new Connect();
            dt = new DataTable();
            SQLiteDataAdapter adtr = new SQLiteDataAdapter(sql, con.Connection());
            adtr.Fill(dt);
            return dt;
        }


    }
}
