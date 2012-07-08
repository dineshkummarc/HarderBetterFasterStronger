using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HarderBetterFasterStronger.Models
{
    public class Set
    {
        public int Repetitions { get; set; }
        public int Weight { get; set; }
        public PlatesPerSide PlatesPerSide { get; set; }

        public Set(int repetitions, int weight)
        {
            this.PlatesPerSide = PlatesPerSide.GetPlatesPerSide(weight);
            this.Repetitions = repetitions;
            this.Weight = weight;
        }
    }
}