using Google.Apis.Auth.OAuth2.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;

namespace OnePieceAbridged.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
