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
            csv.Append("ID");
            csv.Append(comma);
            csv.Append("Press time");
            csv.Append(comma);
            csv.Append("Gap time");
            csv.Append(comma);
            csv.Append("Release/Space");
            csv.Append(comma);
            csv.Append("Space/Press");
            foreach (Person person in persons)
            {
                csv.Append(newLine);
                csv.Append(person.id);
                SecondVector vector = person.secondVector;
                csv.Append(comma);
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
        public static void writeOutputToFile(List<List<PersonVector>> ListpersonsVectors, List<List<Person>> Listpersons, string fileName, int[] thresholds)
        {
            if (!fileName.Contains(".csv"))
            {
                fileName += ".csv";
            }

            StringBuilder csv = new StringBuilder();
            int i = 0;
            foreach (List<PersonVector> vectors in ListpersonsVectors)
            {
                csv.Append(thresholds[i++]);
                foreach (PersonVector vector in vectors)
                {
                    csv.Append(comma);
                    csv.Append(vector.id);
                }
                csv.Append(newLine);
            }
            i = 0;
            foreach (List<Person> vectors in Listpersons)
            {
                csv.Append(thresholds[i++]);
                foreach (Person vector in vectors)
                {
                    csv.Append(comma);
                    csv.Append(vector.id);
                }
                csv.Append(newLine);
            }
            File.WriteAllText(fileName, csv.ToString());
        }
    }
}
