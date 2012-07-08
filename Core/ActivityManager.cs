using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HarderBetterFasterStronger.Models;
using NHibernate;
using HarderBetterFasterStronger.Core.Constants;

namespace HarderBetterFasterStronger.Core
{
    public class ActivityManager
    {
        /// <summary>
        /// Get an activity by id.
        /// </summary>
        /// <param name="id">Id of the desired activity.</param>
        /// <returns></returns>
        public Activity GetActivity(int id)
        {
            Activity activity = null;

            using (ISession session = NHibernateHelper.GetCurrentSession())
            {
                activity = session.Get<Activity>(id);
            }

            return activity;
        }

        /// <summary>
        /// Gets the most recent activity of the specified type.
        /// </summary>
        /// <param name="activityName">Name of the desired activity.</param>
        /// <returns></returns>
        public Activity GetMostRecentActivity(string activityName)
        {
            Activity activity = null;

            using (ISession session = NHibernateHelper.GetCurrentSession())
            {
                 activity = session.QueryOver<Activity>()
                    .OrderBy(x => x.Date).Desc
                    .Where(x => x.Name == activityName)
                    .Take(1)
                    .SingleOrDefault();
            }

            return activity;
        }

        /// <summary>
        /// Saves or updates the specified activity.
        /// </summary>
        /// <param name="activity">The activity to save or update.</param>
        public void Save(Activity activity)
        {
            using (ISession session = NHibernateHelper.GetCurrentSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                session.SaveOrUpdate(activity);
                transaction.Commit();
            }
        }

        /// <summary>
        /// Updates an activity to set is as "complete".
        /// </summary>
        /// <param name="activityId">The id of the activity to update.</param>
        /// <param name="isFailure">Whether the activity was successful or not.</param>
        /// <returns>The updated activity entity.</returns>
        public Activity Complete(int activityId, bool isFailure)
        {
            Activity activity = GetActivity(activityId);
            activity.IsComplete = true;
            activity.IsFailure = isFailure;
            Save(activity);

            return activity;
        }

        /// <summary>
        /// Gets a complete history of all activities.
        /// </summary>
        /// <returns>A dictionary where the key is the name of the activity and the value is a list of all activity records by that name.</returns>
        public IDictionary<string, List<Activity>> GetActivityHistory()
        {
            IDictionary<string, List<Activity>> result = new Dictionary<string, List<Activity>>();
            IList<Activity> allActivities = null;

            using (ISession session = NHibernateHelper.GetCurrentSession())
            {
                allActivities = session.QueryOver<Activity>()
                    .OrderBy(x => x.Date).Asc
                    .List<Activity>();
            }

            return allActivities.GroupBy(x => x.Name).ToDictionary(x => x.Key, x => x.ToList());
        }
    }
}