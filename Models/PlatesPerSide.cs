using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HarderBetterFasterStronger.Models
{
    public class PlatesPerSide
    {
        public int NumberOf2lbPlates { get; set; }
        public int NumberOf5lbPlates { get; set; }
        public int NumberOf10lbPlates { get; set; }
        public int NumberOf25lbPlates { get; set; }
        public int NumberOf35lbPlates { get; set; }
        public int NumberOf45lbPlates { get; set; }

        public static PlatesPerSide GetPlatesPerSide(int weight)
        {
            PlatesPerSide platesPerSide = new PlatesPerSide();
            double weightPerSide = (weight - 45) / 2d; // The bar weighs 45 lbs

            while (weightPerSide / 45 >= 1)
            {
                platesPerSide.NumberOf45lbPlates++;
                weightPerSide -= 45;
            }

            while (weightPerSide / 35 >= 1)
            {
                platesPerSide.NumberOf35lbPlates++;
                weightPerSide -= 35;
            }

            while (weightPerSide / 25 >= 1)
            {
                platesPerSide.NumberOf25lbPlates++;
                weightPerSide -= 25;
            }

            while (weightPerSide / 10 >= 1)
            {
                platesPerSide.NumberOf10lbPlates++;
                weightPerSide -= 10;
            }

            while (weightPerSide / 5 >= 1)
            {
                platesPerSide.NumberOf5lbPlates++;
                weightPerSide -= 5;
            }

            while (weightPerSide / 2.5 >= 1)
            {
                platesPerSide.NumberOf2lbPlates++;
                weightPerSide -= 2.5;
            }

            return platesPerSide;
        }
    }
}