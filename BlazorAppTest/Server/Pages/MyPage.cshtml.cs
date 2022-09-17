using BlazorAppTest.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BlazorAppTest.Server.Pages
{
    public class MyPageModel : PageModel
    {
        public MyClass? MyObject { get; set; }

        public MyPageModel()
        {
            MyObject = new MyClass { Id = 1, Name = "MyPageModel" };
        }
    }
}
