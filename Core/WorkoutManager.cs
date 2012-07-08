using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HarderBetterFasterStronger.Models;
using NHibernate;
using NHibernate.Criterion;
using HarderBetterFasterStronger.Core.Constants;

namespace HarderBetterFasterStronger.Core
{
    public class WorkoutManager
    {
        private ActivityManager activityManager;

        public WorkoutManager()
        {
            activityManager = new ActivityManager();
        }

        /// <summary>
        /// Creates a new workout based on the previous workout.
        /// </summary>
        /// <param name="previousWorkout">The most recent workout done by the user.</param>
        /// <returns></returns>
        private Workout CreateWorkout(Workout previousWorkout)
        {
            Workout workout = new Workout();
            workout.Activities = new List<Activity>();
            workout.Date = DateTime.UtcNow;
            workout.IsComplete = false;

            // Squat is in both workout routines.
            Activity previousSquat = this.activityManager.GetMostRecentActivity(ActivityName.Squat);
            int squatWeight = previousSquat.Weight;

            if (!previousSquat.IsFailure)
            {
                squatWeight += 5;
            }

            workout.AddActivity(new Squat(squatWeight));

            // Add other activities to workout based on routine.
            // Routines alternate between A and B.
            if (previousWorkout.Routine == RoutineName.A)
            {
                workout.Routine = RoutineName.B;

                Activity previousOverheadPress = this.activityManager.GetMostRecentActivity(ActivityName.OverheadPress);
                int overheadPressWeight = previousOverheadPress.Weight;

                if (!previousOverheadPress.IsFailure)
                {
                    overheadPressWeight += 5;
                }

                workout.AddActivity(new OverheadPress(overheadPressWeight));

                Activity previousDeadlift = this.activityManager.GetMostRecentActivity(ActivityName.Deadlift);
                int deadliftWeight = previousDeadlift.Weight;
                
                if (!previousDeadlift.IsFailure)
                {
                    deadliftWeight += 10;
                }

                workout.AddActivity(new Deadlift(deadliftWeight));
            }
            else
            {
                workout.Routine = RoutineName.A;

                Activity previousBenchPress = this.activityManager.GetMostRecentActivity(ActivityName.BenchPress);
                int benchPressWeight = previousBenchPress.Weight;

                if (!previousBenchPress.IsFailure)
                {
                    benchPressWeight += 5;
                }

                workout.AddActivity(new BenchPress(benchPressWeight));
            }

            return workout;
        }

        /// <summary>
        /// Attempts to get an incomplete workout.  If there is no workout in progress it creates a new workout based on the previous one.
        /// </summary>
        /// <returns></returns>
        public Workout GetActiveWorkout()
        {
            Workout workout = null;

            using (ISession session = NHibernateHelper.GetCurrentSession())
            {
                // Check to see if there is an active workout.
                workout = session.QueryOver<Workout>()
                    .Where(x => !x.IsComplete)
                    .SingleOrDefault();
            }

            if (workout == null)
            {
                Workout previousWorkout = null;

                using (ISession session = NHibernateHelper.GetCurrentSession())
                {
                    // If there is no active workout, start a new one.
                    previousWorkout = session.QueryOver<Workout>()
                        .OrderBy(x => x.Date).Desc
                        .Take(1).SingleOrDefault();
                }

                workout = CreateWorkout(previousWorkout);

                using (ISession session = NHibernateHelper.GetCurrentSession())
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(workout);
                    transaction.Commit();
                }
            }
            
            // Populate sets.
            foreach (Activity activity in workout.Activities)
            {
                switch (activity.Name)
                {
                    case ActivityName.BenchPress:
                        BenchPress.PopulateSets(activity);
                        break;
                    case ActivityName.Deadlift:
                        Deadlift.PopulateSets(activity);
                        break;
                    case ActivityName.OverheadPress:
                        OverheadPress.PopulateSets(activity);
                        break;
                    case ActivityName.Squat:
                        Squat.PopulateSets(activity);
                        break;
                }
            }

            return workout;
        }

        /// <summary>
        /// Gets a specific workout by id.
        /// </summary>
        /// <param name="id">Id of the workout to retrieve.</param>
        /// <returns></returns>
        public Workout GetWorkout(int id)
        {
            Workout workout = null;

            using (ISession session = NHibernateHelper.GetCurrentSession())
            {
                workout = session.Get<Workout>(id);
            }

            return workout;
        }

        /// <summary>
        /// Saves or updates the specified workout.
        /// </summary>
        /// <param name="workout">Workout to save or update.</param>
        public void Save(Workout workout)
        {
            using (ISession session = NHibernateHelper.GetCurrentSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                session.SaveOrUpdate(workout);
                transaction.Commit();
            }
        }

        /// <summary>
        /// Attempts to mark a workout as complete.  The workout will only be set as complete if all activities
        /// associated with it are complete.
        /// </summary>
        /// <param name="id">Id of the workout to complete.</param>
        /// <returns>A boolean stating whether the workout was completed or not.</returns>
        public bool Complete(int id)
        {
            Workout workout = GetWorkout(id);
            bool result = false;

            // Check to see if the workout should be completed (all activities completed).
            if (workout.Activities.All(x => x.IsComplete))
            {
                workout.IsComplete = true;
                Save(workout);

                result = true;
            }

            return result;
        }
    }
}