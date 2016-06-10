using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bjometrja2
{
    public class InputData
    {
        public string asciiCode { get; set; }
        public Int64 timeInMilisUp;
        public Int64 timeInMilisDown;
        public int buttonCounter;
        public long averageTime;

        public InputData(string asciiCode, Int64 timeInMilisUp)
        {
            this.asciiCode = asciiCode;
            this.timeInMilisUp = timeInMilisUp;
            this.buttonCounter = 1;
            this.averageTime = 0;
        }

        public InputData(string asciiCode, Int64 timeInMilisDown, string fakeParameter)
        {
            this.asciiCode = asciiCode;
            this.timeInMilisDown = timeInMilisDown;
            this.buttonCounter = 1;
            this.averageTime = 0;
        }

        public InputData()
        {
        }
    }

}
