using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bjometrja2
{
    class DataProcessing
    {
        DBConnect dbConnect { get; set; }

        public DataProcessing(DBConnect dbc)
        {
            this.dbConnect = dbc;

        }
        public void getFirstVectorByUserId(int userId)
        {
            string[] items = getInput1ByUserId(userId);
        }

        public void getSecondVectorByUserId(int userId)
        {
            
        }

        private string[] getInput1ByUserId(int userId)
        {
            DataTable dt = dbConnect.SelectByID(userId);
            string input1 = dt.Rows[3].ItemArray[3].ToString();// pobiera input 1
            return input1.Split(' ');
        }
    }
}
