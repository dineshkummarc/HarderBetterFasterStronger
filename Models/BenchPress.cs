using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HarderBetterFasterStronger.Core;
using HarderBetterFasterStronger.Core.Constants;

namespace HarderBetterFasterStronger.Models
{
    public class BenchPress : Activity
    {
        public BenchPress(int weight) : base(weight)
        {
            this.Name = ActivityName.BenchPress;            
        }

        public static void PopulateSets(Activity activity)
        {
            // Add Warm-up Sets
            // Sets cannot be less than the weight of the bar (45 lbs)
            activity.WarmupSets.Add(new Set(5, 45));
            activity.WarmupSets.Add(new Set(5, 45));
            activity.WarmupSets.Add(new Set(5, Math.Max(45, (int)Math.Floor(activity.Weight * 0.5 / 5) * 5)));
            activity.WarmupSets.Add(new Set(3, Math.Max(45, (int)Math.Floor(activity.Weight * 0.7 / 5) * 5)));
            activity.WarmupSets.Add(new Set(2, Math.Max(45, (int)Math.Floor(activity.Weight * 0.9 / 5) * 5)));

            // Add Work Sets
            activity.WorkSets.Add(new Set(5, activity.Weight));
            activity.WorkSets.Add(new Set(5, activity.Weight));
            activity.WorkSets.Add(new Set(5, activity.Weight));
        }
    }
}