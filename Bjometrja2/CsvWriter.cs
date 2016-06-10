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

        public static void writeToFile(List<PersonVector> persons, string fileName)
        {
            if (!fileName.Contains(".csv")) {
                fileName += ".csv";
            }

            StringBuilder csv = new StringBuilder();

            csv.Append("ID");
            foreach (char letter in alphabet.ToArray())
            {
                csv.Append(comma);
                csv.Append(letter);
            }
            foreach (PersonVector person in persons)
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

        public static void writeSecondVectorToFile(List<Person> persons, string fileName)
        {
            if (!fileName.Contains(".csv"))
            {
                fileName += ".csv";
            }

            StringBuilder csv = new StringBuilder();          
            foreach (Person person in persons)
            {
                csv.Append(newLine);
                csv.Append(person.id);
                SecondVector vector = person.secondVector;
                csv.Append(vector.pressTime);
                csv.Append(comma);
                csv.Append(vector.gapTime);
                csv.Append(comma);
                csv.Append(vector.releaseAndSpaceTime);
                csv.Append(comma);
                csv.Append(vector.releaseSpaceAndPushedButtonTime);
            }
            File.WriteAllText(fileName, csv.ToString());
        }
        public static void writeOutputToFile(List<List<PersonVector>> ListpersonsVectors, List<List<Person>> Listpersons, string fileName, string[] thresholds)
        {
            if (!fileName.Contains(".csv"))
            {
                fileName += ".csv";
            }

            StringBuilder csv = new StringBuilder();
            foreach (List<Person> vectors in Listpersons)
            {
                int i = 0;
                csv.Append(thresholds[i++]);
                csv.Append(comma);
                foreach (Person vector in vectors)
                {
                    csv.Append(vector.id);
                    csv.Append(comma);
                }
                csv.Append(newLine);
            }
            foreach (List<PersonVector> vectors in ListpersonsVectors)
            {
                int i = 0;
                csv.Append(thresholds[i++]);
                csv.Append(comma);
                foreach (PersonVector vector in vectors)
                {
                    csv.Append(vector.id);
                    csv.Append(comma);
                }
                csv.Append(newLine);
            }
            File.WriteAllText(fileName, csv.ToString());
        }
    }
}
