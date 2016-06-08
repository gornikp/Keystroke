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
        public List<InputData> getFirstVectorByUserId(int userId)
        {
            List<InputData> splitted = new List<InputData>();
            foreach(string item in getInput1ByUserId(userId))
            {
                string[] itemSplitted = item.Split('_');
                if(itemSplitted[0] == "d")
                {
                    if(getInputDataByAscii(splitted, itemSplitted[1]) == null)
                    {
                        splitted.Add(new InputData(itemSplitted[1], Convert.ToInt16(itemSplitted[2])));
                    }
                    else
                    {
                        InputData inputData = getInputDataByAscii(splitted, itemSplitted[1]);
                        inputData.timeInMilis += Convert.ToInt16(itemSplitted[2]);
                    }
                }
            }
            return splitted;
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

        private InputData getInputDataByAscii(List<InputData> listInputData, string ascii)
        {
            return listInputData.Find(x => x.asciiCode == ascii);
        }
    }
}
