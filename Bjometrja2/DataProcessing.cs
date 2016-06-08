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
            List<InputData> groupedInputData = new List<InputData>();
            foreach(string item in getInput1ByUserId(userId))
            {
                string[] itemSplitted = item.Split('_');
                if (itemSplitted[0] != "")
                {
                    if (getInputDataByAscii(groupedInputData, itemSplitted[1]) == null)
                    {
                        if (itemSplitted[0] == "u")
                        {
                            groupedInputData.Add(new InputData(itemSplitted[1], Convert.ToInt16(itemSplitted[2])));
                        }
                        if (itemSplitted[0] == "d")
                        {
                            groupedInputData.Add(new InputData(itemSplitted[1], Convert.ToInt16(itemSplitted[2]), null));
                        }
                    }
                    else
                    {
                        InputData inputData = getInputDataByAscii(groupedInputData, itemSplitted[1]);
                        if (itemSplitted[0] == "u")
                        {
                            inputData.timeInMilisUp += Convert.ToInt16(itemSplitted[2]);
                        }
                        if (itemSplitted[0] == "d")
                        {
                            inputData.timeInMilisDown += Convert.ToInt16(itemSplitted[2]);
                            inputData.buttonCounter++;
                        }
                    }
                }
            }
            foreach(InputData inputData in groupedInputData)
            {
                inputData.averageTime = ((inputData.timeInMilisUp - inputData.timeInMilisDown) / inputData.buttonCounter);
            }
            return groupedInputData;
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
