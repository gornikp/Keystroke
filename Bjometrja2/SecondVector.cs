using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bjometrja2
{
    class SecondVector
    {
        public long pressTime { get; set; }
        public long gapTime;
        public long releaseAndSpaceTime;
        public long releaseSpaceAndPushedButtonTime;

        public SecondVector ()
        {
            this.pressTime = 0;
            this.gapTime = 0;
            this.releaseAndSpaceTime = 0;
            this.releaseSpaceAndPushedButtonTime = 0;
        }

        public SecondVector withPressTime(long pressTime)
        {
            this.pressTime = pressTime;
            return this;
        }

        public SecondVector withGapTime(long gapTime)
        {
            this.gapTime = gapTime;
            return this;
        }

        public SecondVector withReleaseAndSpaceTime(long releaseAndSpaceTime)
        {
            this.releaseAndSpaceTime = releaseAndSpaceTime;
            return this;
        }

        public SecondVector withreleaseSpaceAndPushedButtonTime(long releaseSpaceAndPushedButtonTime)
        {
            this.releaseSpaceAndPushedButtonTime = releaseSpaceAndPushedButtonTime;
            return this;
        }
    }
}
