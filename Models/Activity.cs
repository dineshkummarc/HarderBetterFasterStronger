using System;

namespace HarderBetterFasterStronger.Models
{
    public class Activity
    {
        public virtual int Id { get; set; }
        public virtual Guid UserId { get; set; }
        public virtual string Name { get; set; }
        public virtual int Weight { get; set; }
        public virtual bool IsFailure { get; set; }
        public virtual DateTime Created { get; set; }
    }
}