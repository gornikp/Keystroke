using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bjometrja2
{
     class VectorComparingSystem
    {
        private static string alphabet = "abcdefghijklmnopqrstuvwxyz".ToUpper();
        public static List<PersonVector> CompareFirstVectors(List<PersonVector> persons, long[] vector)
        {
            List<PersonVector> personsCopy = persons;
            foreach (var person in personsCopy)
            {
                long sum = 0;
                foreach (char letter in alphabet.ToArray())
                {
                    long current = 0;
                    string key = letter.ToString();
                    InputData data = new InputData();
                    person.firstVector.TryGetValue(key, out data);
                    if (data == null)
                    {
                        current = 0;
                    }
                    else
                    {
                        current = data.averageTime;
                    }
                    if (current != 0 && vector[letter - 65] != 0)
                    {
                        sum += Math.Abs(current - vector[letter - 65]);
                    }
                }
                person.distance = sum;
            }
              List<PersonVector> output = personsCopy.OrderBy(x => x.distance).ToList();
            return new List<PersonVector> { output[0], output[1], output[2] };
        }
        public static List<Person> CompareSecondVectors(List<Person> persons, long[] vector)
        {
            List<Person> personsCopy = persons;
            foreach (var person in personsCopy)
            {
                double sum = Math.Pow((double)(person.secondVector.pressTime - vector[0]),2) + Math.Pow((double)(person.secondVector.gapTime - vector[1]),2) + Math.Pow((double)(person.secondVector.releaseAndSpaceTime - vector[2]),2) + Math.Pow((double)(person.secondVector.releaseSpaceAndPushedButtonTime - vector[3]),2);
                sum = Math.Sqrt(sum);
                person.distance = Convert.ToInt64(sum);
            }
            List<Person> output = personsCopy.OrderBy(x => x.distance).ToList();

            return new List<Person> { output[0], output[1], output[2] };
        }
    }
}
