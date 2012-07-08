using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HarderBetterFasterStronger.Core;
using HarderBetterFasterStronger.Core.Constants;

namespace HarderBetterFasterStronger.Models
{
    public class OverheadPress : Activity
    {
        public OverheadPress(int weight) : base(weight)
        {
            this.Name = ActivityName.OverheadPress;
        }

        public static void PopulateSets(Activity activity)
        {
            // Add Warm-up Sets
            // Sets cannot be less than the weight of the bar (45 lbs)
            activity.WarmupSets.Add(new Set(5, 45));
            activity.WarmupSets.Add(new Set(5, 45));
            activity.WarmupSets.Add(new Set(5, Math.Max(45, (int)Math.Floor(activity.Weight * 0.55 / 5) * 5)));
            activity.WarmupSets.Add(new Set(3, Math.Max(45, (int)Math.Floor(activity.Weight * 0.7 / 5) * 5)));
            activity.WarmupSets.Add(new Set(2, Math.Max(45, (int)Math.Floor(activity.Weight * 0.85 / 5) * 5)));

            // Add Work Sets
            activity.WorkSets.Add(new Set(5, activity.Weight));
            activity.WorkSets.Add(new Set(5, activity.Weight));
            activity.WorkSets.Add(new Set(5, activity.Weight));
        }
    }
}