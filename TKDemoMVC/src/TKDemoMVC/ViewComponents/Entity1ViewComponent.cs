using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TKDemoMVC.Models;

namespace TKDemoMVC.ViewComponents
{
    [ViewComponent(Name = "Entity1View")]
    public class Entity1ViewComponent : ViewComponent
    {
        private readonly IRepository<Entity1> _data;

        public Entity1ViewComponent(RepositoryMem<Entity1> rep1) {
            _data = rep1;
        }

        public IViewComponentResult Invoke(string title) {
            ViewBag.Title = title;
            return View(_data.Get());
        }
    }
}
