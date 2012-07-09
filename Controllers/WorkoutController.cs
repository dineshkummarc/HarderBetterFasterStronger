using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HarderBetterFasterStronger.Models;
using NHibernate;
using HarderBetterFasterStronger.Core;
using HarderBetterFasterStronger.Core.Constants;

namespace HarderBetterFasterStronger.Controllers
{
    public class WorkoutController : Controller
    {
        private WorkoutManager workoutManager;
        private ActivityManager activityManager;

        public WorkoutController()
        {
            workoutManager = new WorkoutManager();
            activityManager = new ActivityManager();
        }

        public ActionResult Index()
        {
            Workout workout = workoutManager.GetActiveWorkout();

            return View(workout);
        }

        [HttpPost]
        public ContentResult CompleteActivity(int activityId, bool isFailure)
        {
            ContentResult result;

            // Update the status of the activity.
            Activity activity = activityManager.Complete(activityId, isFailure);
            
            // Update the status of the workout.
            bool isWorkoutComplete = workoutManager.Complete(activity.Workout.Id);

            if (isWorkoutComplete)
            {
                result = Content(CompleteActivityStatus.WorkoutComplete);
            }
            else
            {
                result = Content(CompleteActivityStatus.ActivityComplete);
            }

            return result;
        }
    }
}