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
            Int64 czasOdcisniecia = 0; 
            Int64 spacjaUp = 0;
            Int64 spacjaDown = 0;
            Int64 poprzedniUp = 0;
            Int64 nastepnyDown = 0;
            Int64 trzeciaSrednia = 0;
            Int64 czwartaSrednia = 0;
            Int64 spacjaCounter = 0;
            int iloscBadan = 0;
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
                            if (splittedSplittedItem[1] == "32")
                            {
                                spacjaUp = Convert.ToInt64(splittedSplittedItem[2]);
                            }
                            else
                            {
                                poprzedniUp = Convert.ToInt64(splittedSplittedItem[2]);
                            }
                            czasOdcisniecia += Convert.ToInt64(splittedSplittedItem[2]);
                            wcisniecie = Convert.ToInt64(splittedSplittedItem[2]);
                            buttonCounter++;
                        }
                        if (splittedSplittedItem[0] == "d")
                        {
                            if (splittedSplittedItem[1] == "32")
                            {
                                spacjaDown = Convert.ToInt64(splittedSplittedItem[2]);
                                trzeciaSrednia += spacjaDown - poprzedniUp;
                                spacjaCounter++;
                                spacjaDown = 0;
                            }
                            else
                            {
                                if (spacjaUp != 0)
                                {
                                    nastepnyDown = Convert.ToInt64(splittedSplittedItem[2]);
                                    czwartaSrednia += nastepnyDown - spacjaUp;
                                    spacjaCounter++;
                                    spacjaUp = 0;
                                }
                            }
                            czasWcisniecia += Convert.ToInt64(splittedSplittedItem[2]);
                            sumaGap += Convert.ToInt64(splittedSplittedItem[2]) - poprzedni;
                            poprzedni = Convert.ToInt64(splittedSplittedItem[2]);
                        }

                    }
                }
                sv.gapTime += sumaGap;
                sv.pressTime += (czasOdcisniecia - czasWcisniecia) / buttonCounter;
                iloscBadan++;
            }
            if (spacjaCounter != 0) { 
            sv.releaseAndSpaceTime = trzeciaSrednia / spacjaCounter;
            sv.releaseSpaceAndPushedButtonTime = czwartaSrednia / spacjaCounter;
             }
             sv.pressTime = (czasOdcisniecia - czasWcisniecia) / buttonCounter;
            if (buttonCounter != 0)
            {
                sv.gapTime = sv.gapTime / (buttonCounter - 1);
                sv.pressTime = sv.pressTime / iloscBadan;
            }
            return sv;
        }

        private long getPressTime(string input)
        {
            long pressTime = 0;
            return pressTime;

        }

    }


}
