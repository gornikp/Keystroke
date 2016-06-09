﻿using System;
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
        public long[] secondVector { get; set; }

        public Person(string id, Dictionary<string, InputData> firstVector)
        {
            this.id = id;
            this.firstVector = firstVector;
        }
    }
}
