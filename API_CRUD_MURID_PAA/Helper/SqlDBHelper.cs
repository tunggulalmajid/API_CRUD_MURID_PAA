using Npgsql;

namespace API_CRUD_MURID_PAA.Helper
{
    public class SqlDBHelper
    {
        private NpgsqlConnection conn;
        private string connStr;

        public SqlDBHelper(string connStr)
        {
            this.connStr = connStr;
            conn = new NpgsqlConnection(connStr);
        }

        public NpgsqlCommand GetNpgsqlCommand(string query)
        {

            conn.Open();
            NpgsqlCommand cmd = new NpgsqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = query;
            cmd.CommandType = System.Data.CommandType.Text;
            return cmd;
        }



        public void closeConnection()
        {
            conn.Close();
        }


    }
}
