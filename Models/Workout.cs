using System;
using System.Collections.Generic;

namespace HarderBetterFasterStronger.Models
{
    public class Workout
    {
        public virtual int Id { get; set; }
        public virtual string Routine { get; set; }
        public virtual bool IsComplete { get; set; }
        public virtual DateTime Date { get; set; }
        public virtual IList<Activity> Activities { get; set; }

        public virtual void AddActivity(Activity activity)
        {
            activity.Workout = this;
            this.Activities.Add(activity);
        }
    }
}