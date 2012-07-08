using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HarderBetterFasterStronger.Core;
using HarderBetterFasterStronger.Core.Constants;

namespace HarderBetterFasterStronger.Models
{
    public class Deadlift : Activity
    {
        public Deadlift(int weight) : base(weight)
        {
            this.Name = ActivityName.Deadlift;
        }

        public static void PopulateSets(Activity activity)
        {
            // Add Warm-up Sets
            // Sets cannot be less than the weight of the bar (45 lbs)
            activity.WarmupSets.Add(new Set(5, Math.Max(45, (int)Math.Floor(activity.Weight * 0.4 / 5) * 5)));
            activity.WarmupSets.Add(new Set(5, Math.Max(45, (int)Math.Floor(activity.Weight * 0.4 / 5) * 5)));
            activity.WarmupSets.Add(new Set(3, Math.Max(45, (int)Math.Floor(activity.Weight * 0.6 / 5) * 5)));
            activity.WarmupSets.Add(new Set(2, Math.Max(45, (int)Math.Floor(activity.Weight * 0.85 / 5) * 5)));

            // Add Work Sets
            activity.WorkSets.Add(new Set(5, activity.Weight));
        }
    }
}