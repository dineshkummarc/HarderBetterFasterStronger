using System;

namespace HarderBetterFasterStronger.Models
{
    public class Workout
    {
        public virtual int Id { get; set; }
        public virtual Guid UserId { get; set; }
        public virtual string Routine { get; set; }
        public virtual DateTime Created { get; set; }
    }
}