using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QAScraper.Controllers
{
    using System.IO;

    using QAScraper.Helpers;

    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View(FileHelper.GetFiles());
        }


        [HttpPost]
        public ActionResult Update()
        {
            FileHelper.UpdateSites();
            return Json(true);
        }
    }
}
