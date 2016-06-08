using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bjometrja2
{
    class InputData
    {
        public string asciiCode { get; set; }
        public int timeInMilis;

        public InputData(string asciiCode, int timeInMilis)
        {
            this.asciiCode = asciiCode;
            this.timeInMilis = timeInMilis;
        }

    }

}
