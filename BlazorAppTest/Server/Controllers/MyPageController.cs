using BlazorAppTest.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlazorAppTest.Server.Controllers
{
    public class MyPageController : Controller
    {
        public IActionResult Index()
        {
            var vm = new MyClass();
            return View(vm);
        }
    }
}
