using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Funhall2.Classes
{
    public class DAL
    {
        SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=FunHall;" + "Integrated Security=true;");
        private SqlParameter CreateParam(string name, object value, SqlDbType type)
        {
            SqlParameter param = new SqlParameter(name, type);
            param.Value = value;
            return param;
        }
        public void UpdateCusActivity(int id, string points, string activityName)
        {
            con.Open();
            SqlCommand command = new SqlCommand("UPDATE GuestActivities SET Points = @Points WHERE GuestID = @ID AND TimeDesc = @ActivityName", con);
            command.Parameters.Add(CreateParam("@ID", id, SqlDbType.NVarChar));
            command.Parameters.Add(CreateParam("@Points", points, SqlDbType.NVarChar));
            command.Parameters.Add(CreateParam("@ActivityName", activityName, SqlDbType.NVarChar));
            command.ExecuteNonQuery();
            con.Close();
        }

    }
}
