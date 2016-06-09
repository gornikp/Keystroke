using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bjometrja2
{
    class Person
    {
        public string id { get; set; }
        public Dictionary<string, InputData> firstVector { get; set; }
        public SecondVector secondVector { get; set; }

        public Person withId(string id)
        {
            this.id = id;
            return this;
        }

        public Person withFirstVector(Dictionary<string, InputData> firstVector)
        {
            this.firstVector = firstVector;
            return this;
        }

        public Person withSecondVector(SecondVector secondVector)
        {
            this.secondVector = secondVector;
            return this;
        }
    }
}
