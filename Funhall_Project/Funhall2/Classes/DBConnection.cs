using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;



namespace Funhall2.Classes
{
    public class DBConnection
    {
        public SqlConnection con;

        public DBConnection()
        {
             con = new SqlConnection("Data Source=.;Initial Catalog=FunHall;"
                                + "Integrated Security=true;");
             con.Open();
        }

        public void ConnectionClose()
        {
            con.Close();
        }
    }
}
