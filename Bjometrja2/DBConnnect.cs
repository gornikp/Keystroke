using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Bjometrja2
{
    class DBConnect
    {
        private MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;

        //Constructor
        public DBConnect()
        {
            Initialize();
        }

        //Initialize values
        private void Initialize()
        {
            server = "karanko.pl";
            database = "bio";
            uid = "bio";
            password = "siemanko";
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

            connection = new MySqlConnection(connectionString);
        }

        //open connection to database
        private bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                //When handling errors, you can your application's response based 
                //on the error number.
                //The two most common error numbers when connecting are as follows:
                //0: Cannot connect to server.
                //1045: Invalid user name and/or password.
                        MessageBox.Show(ex.ToString());
                return false;
            }
        }

        //Close connection
        private bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public DataTable SelectAll()
        {
            string query = "SELECT * FROM tx_badanie01 ORDER BY user_id,time";
            if (this.OpenConnection() == true)
            {

                MySqlCommand cmd = new MySqlCommand(query, connection);

                //MySqlDataReader dataReader = cmd.ExecuteReader();              
                //while (dataReader.Read())
                //{
                //    list[0].Add(dataReader["user_id"] + "");
                //    list[1].Add(dataReader["input0"] + "");
                //    list[2].Add(dataReader["input1"] + "");
                //    list[3].Add(dataReader["IP"] + "");

                //}
                //dataReader.Close();

                DataTable dt = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                this.CloseConnection();

                return dt;
            }
            else
            {
                return null;
            }
        }
        public DataTable SelectByID(int id)
        {
            string query = "SELECT * FROM tx_badanie01 WHERE user_id = " + id;
            if (this.OpenConnection() == true)
            {
                
                MySqlCommand cmd = new MySqlCommand(query, connection);
               
                //MySqlDataReader dataReader = cmd.ExecuteReader();              
                //while (dataReader.Read())
                //{
                //    list[0].Add(dataReader["user_id"] + "");
                //    list[1].Add(dataReader["input0"] + "");
                //    list[2].Add(dataReader["input1"] + "");
                //    list[3].Add(dataReader["IP"] + "");

                //}
                //dataReader.Close();

                DataTable dt = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);              
                this.CloseConnection();

                return dt;
            }
            else
            {
                return null;
            }
        }
    }
}
