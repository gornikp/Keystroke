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
        private static string alphabet = "abcdefghijklmnopqrstuvwxyz";

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
                List<InputData> vector = person.firstVector;
                foreach (InputData data in vector)
                {
                    csv.Append(comma);
                    csv.Append(data.averageTime);
                    Int16 ascii = Int16.Parse(data.asciiCode);
                    char character = (char)ascii;
                    Console.WriteLine(character);
                }
                if (vector.Count == alphabet.Count())
                {
                    Console.WriteLine("26 znaków");
                }
                else
                {
                    Console.WriteLine("ej za mało znaków!");
                }
            }
            File.WriteAllText(fileName, csv.ToString());
        }
    }
}
