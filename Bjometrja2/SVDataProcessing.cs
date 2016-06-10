using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bjometrja2
{
    class SVDataProcessing : DataProcessing
    {
        public SVDataProcessing(DBConnect dbc) : base(dbc)
        {
        }

        public SecondVector getSecondVectorById(string id)
        {
            SecondVector sv = new SecondVector();
            int buttonCounter = 0;
            Int64 czasWcisniecia = 0;
            Int64 spacjaUp = 0;
            Int64 spacjaDown = 0;
            Int64 poprzedniUp = 0;
            Int64 poprzedniDown = 0;
            Int64 trzeciaSrednia = 0;
            Int64 spacjaCounter = 0;
            foreach (string input in getInput1ByUserId(Convert.ToInt16(id)))
            {
                Int64 poprzedni = 0;
                Int64 sumaGap = 0;
                string[] split = input.Split(' ');
                foreach (string splittedItem in split)
                {
                    Int64 wcisniecie = 0;

                    string[] splittedSplittedItem = splittedItem.Split('_');
                    if (splittedSplittedItem[0] != "")
                    {
                        if (splittedSplittedItem[0] == "u")
                        {
                            if(splittedSplittedItem[1] == "32")
                            {
                                spacjaUp = Convert.ToInt64(splittedSplittedItem[2]);
                            }
                            else
                            {
                                poprzedniUp = Convert.ToInt64(splittedSplittedItem[2]);
                            }
                            wcisniecie = Convert.ToInt64(splittedSplittedItem[2]);
                            buttonCounter++;
                        }
                        if (splittedSplittedItem[0] == "d")
                        {
                            if (splittedSplittedItem[1] == "32")
                            {
                                spacjaDown = Convert.ToInt64(splittedSplittedItem[2]);
                                spacjaCounter++;
                            }
                            else
                            {
                                poprzedniDown = Convert.ToInt64(splittedSplittedItem[2]);
                            }
                            czasWcisniecia += Convert.ToInt64(splittedSplittedItem[2]);
                            sumaGap += Convert.ToInt64(splittedSplittedItem[2]) - poprzedni;
                        }
                        if(spacjaDown != 0)
                        {
                            trzeciaSrednia += spacjaDown - poprzedniUp;
                            spacjaDown = 0;
                        }
                        poprzedni = Convert.ToInt64(splittedSplittedItem[2]);
                        spacjaUp = 0;
                    }
                }
                sv.gapTime += sumaGap;
            }
            sv.releaseAndSpaceTime = trzeciaSrednia / spacjaCounter;
            // sv.pressTime = (czasOdcisniecia - czasWcisniecia) / buttonCounter;
            sv.gapTime = sv.gapTime / buttonCounter;
            return sv;
        }

        private long getPressTime(string input)
        {
            long pressTime = 0;
            return pressTime;

        }

    }


}
