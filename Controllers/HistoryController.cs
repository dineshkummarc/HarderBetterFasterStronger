using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HarderBetterFasterStronger.Core;

namespace HarderBetterFasterStronger.Controllers
{
    public class HistoryController : Controller
    {
        private ActivityManager activityManager;

        public HistoryController()
        {
            activityManager = new ActivityManager();
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}
