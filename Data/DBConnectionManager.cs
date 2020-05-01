using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserDirectory.Data
{
    public sealed class DBConnectionManager
    {
        #region Singleton
        private readonly string connectionstring;

        private static DBConnectionManager instance;

        public static DBConnectionManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DBConnectionManager();
                }
                return instance;
            }

        } 
        #endregion

        #region Ctor
        private DBConnectionManager()
        {
            connectionstring = ConfigurationManager.ConnectionStrings["UserDirectoryDBEntities"].ConnectionString;
        }
        #endregion

        #region Public Methods
        public DataTable GetData(string strQry)
        {
            DataTable dt;
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand(strQry, con);
                cmd.CommandType = CommandType.Text;

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                dt = new DataTable();
                sda.Fill(dt);
            }

            return dt;
        } 
        #endregion
    }
}
