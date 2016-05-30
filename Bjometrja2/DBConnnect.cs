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

        public List<List<string>> SelectAll()
        {
            string query = "SELECT * FROM tx_badanie01";

            //Create a list to store the result
            List<List<string>> list = new List<List<string>>(4);
            list[0] = new List<string>();
            list[1] = new List<string>();
            list[2] = new List<string>();
            list[3] = new List<string>();

            //Open connection
            if (this.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    list[0].Add(dataReader["user_id"] + "");
                    list[1].Add(dataReader["input0"] + "");
                    list[2].Add(dataReader["input1"] + "");
                    list[3].Add(dataReader["IP"] + "");

                }
                //close Data Reader
                dataReader.Close();

                //close Connection
                this.CloseConnection();

                //return list to be displayed
                return list;
            }
            else
            {
                return list;
            }
        }
        public DataTable SelectByID(int id)
        {
            string query = "SELECT * FROM tx_badanie01 WHERE user_id = " + id;

            List<List<string>> list = new List<List<string>>(4);
            list.Add(new List<string>());
            list.Add(new List<string>());
            list.Add(new List<string>());
            list.Add(new List<string>());

            if (this.OpenConnection() == true)
            {
                //Create Command
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
