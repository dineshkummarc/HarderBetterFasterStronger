using System;
using System.Collections.Generic;

namespace HarderBetterFasterStronger.Models
{
    public class Activity
    {
        public virtual int Id { get; set; }
        public virtual Workout Workout { get; set; }
        public virtual string Name { get; set; }
        public virtual int Weight { get; set; }
        public virtual bool IsComplete { get; set; }
        public virtual bool IsFailure { get; set; }
        public virtual IList<Set> WarmupSets { get; set; }
        public virtual IList<Set> WorkSets { get; set; }
        public virtual DateTime Date { get; set; }

        public Activity()
        {
            this.Date = DateTime.UtcNow;
            this.IsComplete = false;
            this.IsFailure = false;

            this.WarmupSets = new List<Set>();
            this.WorkSets = new List<Set>();
        }

        public Activity(int weight)
            : this()
        {
            this.Weight = weight;
        }
    }
}