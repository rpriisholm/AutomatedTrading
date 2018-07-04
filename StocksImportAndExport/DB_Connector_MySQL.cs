using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stocks
{
    public class DB_Connector_MySQL
    {
        public MySqlConnection Connection { get; set; }
        private string Server { get; set; }
        private string Database { get; set; }
        private string User { get; set; }
        private string Password { get; set; }
        private string ConnectionString { get; set; }

        // Constructor
        public DB_Connector_MySQL(string server, string database, string user, string password)
        {
            this.Server = server;
            this.Database = database;
            this.User = user;
            this.Password = password;

            ConnectionString = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + user + ";" + "PASSWORD=" + password + ";";
            this.Connection = new MySqlConnection(ConnectionString);
        }

        public static DB_Connector_MySQL GetStocksConnector()
        {
            return new DB_Connector_MySQL("127.0.0.1", "stocksdb", "rpriisholm", "Kodenerny01");
        }


        //open connection to database
        private bool OpenConnection()
        {
            try
            {
                Connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                switch (ex.Number)
                {
                    case 0:
                        throw new Exception("Cannot connect to server. Contact administrator.");
                    case 1045:
                        throw new Exception("Invalid username/password, please try again");
                }
                return false;
            }
        }

        //Close connection
        private void CloseConnection()
        {
            Connection.Close();
        }

        //Select statement
        public void User_check(string username, string password)
        {
            string query = "SELECT * FROM swear_tool WHERE username =" + username;

            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, Connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                dataReader.Close();
                this.CloseConnection();
            }
        }

        public MySqlConnection CreateConnection()
        {
            return new MySqlConnection(ConnectionString);
        }
    }
}
