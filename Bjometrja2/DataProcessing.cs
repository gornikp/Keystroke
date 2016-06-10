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

        public Dictionary<string, InputData> getFirstVectorByInput(string input) 
        {
            Dictionary<string, InputData> groupedInputData = new Dictionary<string, InputData>();

                string[] split = input.Split(' ');
                foreach (string splittedItem in split)
                {
                    string[] splittedSplittedItem = splittedItem.Split('_');
                    if (splittedSplittedItem[0] != "" && isChar(splittedSplittedItem[1]))
                    {
                        splittedSplittedItem[1] = parseToChar(splittedSplittedItem[1]);

                        if (getInputDataByAscii(groupedInputData, splittedSplittedItem[1]) == null)
                        {
                            if (splittedSplittedItem[0] == "u")
                            {
                                groupedInputData.Add(splittedSplittedItem[1], new InputData(splittedSplittedItem[1], Convert.ToInt64(splittedSplittedItem[2])));
                            }
                            if (splittedSplittedItem[0] == "d")
                            {
                                groupedInputData.Add(splittedSplittedItem[1], new InputData(splittedSplittedItem[1], Convert.ToInt64(splittedSplittedItem[2]), null));
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
            Dictionary<string, InputData>.ValueCollection values = groupedInputData.Values;
            foreach (InputData inputData in values)
            {
                inputData.averageTime = ((inputData.timeInMilisUp - inputData.timeInMilisDown) / inputData.buttonCounter);
            }
            return groupedInputData;
        }

        protected string parseToChar(string character)
        {
            char newchar = (char)Convert.ToInt16(character);
            return Convert.ToString(newchar);
        }

        protected string parseToLower(string character)
        {
            int convertedChar = Convert.ToInt16(character);
            if (convertedChar <= 90 && convertedChar >= 65)
            {
                return Convert.ToString(convertedChar + 32);
            }
            else return character;
        }

        protected Boolean isChar(string character)
        {
            return (isLowerLetter(character) || isBiggerLetter(character));
        }

        protected Boolean isLowerLetter(string character)
        {
            return Convert.ToInt16(character) >= 97 && Convert.ToInt16(character) <= 122;
        }

        protected Boolean isBiggerLetter(string character)
        {
            return Convert.ToInt16(character) >= 65 && Convert.ToInt16(character) <= 90;
        }

        public void getSecondVectorByUserId(int userId)
        {
            
        }

        public List<string> getInput1ByUserId(int userId)
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

        protected InputData getInputDataByAscii(Dictionary<string, InputData> inputData, string ascii)
        {
            InputData data = new InputData();
            inputData.TryGetValue(ascii, out data);
            return data;
        }
    }
}
