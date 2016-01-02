using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TKDemoMVC.Models;

namespace TKDemoMVC.Controllers {
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// http://www.asp.net/vnext/overview/aspnet-vnext/create-a-web-api-with-mvc-6
    /// </remarks>
    [Route("api/[controller]")]
    public class TodoApi : Controller {
        //This WILL NOT WORK in ASP.NET 5 - NO STATICs!!! (at least)
        static readonly List<TodoItem> _itemsWrong = new List<TodoItem>()
            {
            new TodoItem { Id = 1, Title = "First Item" }
        };
        List<TodoItem> _items;
        public TodoApi(List<TodoItem> _lst ) {
            _items = _lst;
        }

        [HttpGet]
        public IEnumerable<TodoItem> GetAll() {
            return _items;
        }

        [HttpGet("{id:int}", Name = "GetByIdRoute")]
        public IActionResult GetById(int id) {
            var item = _items.FirstOrDefault(x => x.Id == id);
            if (item == null) {
                return HttpNotFound();
            }

            return new ObjectResult(item);
        }
        //{"Id":2,"Title":"First Item","IsDone":false}
        [HttpPost]
        public void CreateTodoItem([FromBody] TodoItem item) {
            if (!ModelState.IsValid) {
                Response.StatusCode = 400;
            } else {
                item.Id = 1 + _items.Max(x => (int?)x.Id) ?? 0;
                _items.Add(item);
                //Dynamic route generation
                string url = Url.RouteUrl("GetByIdRoute", new { id = item.Id },
                    Request.Scheme, Request.Host.ToUriComponent());

                Response.StatusCode = 201;
                Response.Headers["Location"] = url;
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteItem(int id) {
            var item = _items.FirstOrDefault(x => x.Id == id);
            if (item == null) {
                return HttpNotFound();
            }
            _items.Remove(item);
            return new HttpStatusCodeResult(204); // 201 No Content
        }
    }
}

