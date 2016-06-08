using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bjometrja2
{
    class Person
    {
        string id { get; set; }
        List<InputData> firstVector { get; set; }
        long[] secondVector { get; set; }

        public Person(string id, List<InputData> firstVector)
        {
            this.id = id;
            this.firstVector = firstVector;
        }
    }
}
