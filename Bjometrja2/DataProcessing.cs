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
                string[] split = item.Split(' ');
                foreach (string splittedItem in split)
                {
                    string[] splittedSplittedItem = splittedItem.Split('_');
                    if (splittedSplittedItem[0] != "")
                    {
                        if (getInputDataByAscii(groupedInputData, splittedSplittedItem[1]) == null)
                        {
                            if (splittedSplittedItem[0] == "u")
                            {
                                groupedInputData.Add(new InputData(splittedSplittedItem[1], Convert.ToInt64(splittedSplittedItem[2])));
                            }
                            if (splittedSplittedItem[0] == "d")
                            {
                                groupedInputData.Add(new InputData(splittedSplittedItem[1], Convert.ToInt64(splittedSplittedItem[2]), null));
                            }
                        }
                        else
                        {
                            InputData inputData = getInputDataByAscii(groupedInputData, splittedSplittedItem[1]);
                            if (splittedSplittedItem[0] == "u")
                            {
                                inputData.timeInMilisUp += Convert.ToInt64(splittedSplittedItem[2]);
                            }
                            if (splittedSplittedItem[0] == "d")
                            {
                                inputData.timeInMilisDown += Convert.ToInt64(splittedSplittedItem[2]);
                                inputData.buttonCounter++;
                            }
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

        private List<string> getInput1ByUserId(int userId)
        {
            DataTable dt = dbConnect.SelectByID(userId);
            List<string> inputsOfUserById = new List<string>();
            for (int i=0;i<dt.Rows.Count; i++)
            {
                inputsOfUserById.Add(dt.Rows[i].ItemArray[3].ToString());
            }
            return inputsOfUserById;
        }
        public string[] getUserIds()
        {
            string[] userIds = new string[300];
            DataTable dt = dbConnect.SelectAll();
            DataRowCollection ss = dt.Rows;
            foreach (DataRow dr in dt.Rows)
            {
                userIds[Convert.ToInt16(dr.ItemArray[1])] = dr.ItemArray[1].ToString();
            }
            return userIds;
        }

        private InputData getInputDataByAscii(List<InputData> listInputData, string ascii)
        {
            return listInputData.Find(x => x.asciiCode == ascii);
        }
    }
}
