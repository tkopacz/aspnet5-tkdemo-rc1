using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TKDemoMVC.Models;

namespace TKDemoMVC.Controllers
{
    public class DiRepositoryController: Controller {
        IRepository<Entity1> repository;

        ///Must be public property - do
        [FromServices]
        public IRepository<Entity1> repository1 {
            get; set;
        }

        public DiRepositoryController(RepositoryMem<Entity1> rep1,IRepository<Entity1> irep1) {
            rep1.Add(new Entity1() { Title = DateTime.Now.Ticks.ToString() }); //Singleton
            irep1.Add(new Entity1() { Title = DateTime.Now.Ticks.ToString() }); //Transient
            repository = rep1;
        }

        public IActionResult Index() {
            return View(repository.Get()); 
        }
    }
}
