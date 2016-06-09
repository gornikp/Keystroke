using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bjometrja2
{
    class CsvWriter
    {
        private static string comma = ",";
        private static string newLine = "\n";
        private static string alphabet = "abcdefghijklmnopqrstuvwxyz".ToUpper();

        public static void writeToFile(List<Person> persons, string fileName)
        {
            if (!fileName.Contains(".csv")) {
                fileName += ".csv";
            }

            var csv = new StringBuilder();

            csv.Append("ID");
            foreach (char letter in alphabet.ToArray())
            {
                csv.Append(comma);
                csv.Append(letter);
            }

            foreach (Person person in persons)
            {
                csv.Append(newLine);
                csv.Append(person.id);
                Dictionary<string, InputData> vector = person.firstVector;
                foreach (char letter in alphabet.ToArray())
                {
                    string key = letter.ToString();
                    InputData data = new InputData();
                    vector.TryGetValue(key, out data);
                    csv.Append(comma);
                    if (data == null)
                    {
                        csv.Append(0);
                    }
                    else
                    {
                        csv.Append(data.averageTime);
                    }
                }
            }
            File.WriteAllText(fileName, csv.ToString());
        }
    }
}
