using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TKDemoMVC.Controllers
{
    public class CacheController:Controller
    {
        public IActionResult Index() {
            return View();
        }
    }
}
