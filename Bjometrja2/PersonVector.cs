using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bjometrja2
{
    public class PersonVector
    {

        public string id { get; set; }
        public Dictionary<string, InputData> firstVector { get; set; }
        public long distance { get; set; }

        public PersonVector withId(string id)
        {
            this.id = id;
            return this;
        }

        public PersonVector withFirstVector(Dictionary<string, InputData> firstVector)
        {
            this.firstVector = firstVector;
            return this;
        }
      
    }
}
