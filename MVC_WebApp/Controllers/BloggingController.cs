using Microsoft.AspNetCore.Mvc;
using MVC_WebApp.Data;

namespace MVC_WebApp.Controllers
{
    public class BloggingController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public BloggingController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public IActionResult Index()
        {
            var data= _applicationDbContext.BlogPostCounts.ToList();
            return View(data);
        }
    }
}
