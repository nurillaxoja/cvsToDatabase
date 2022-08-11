using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using Task.Models;

namespace Task.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        [HttpPost]
        //[Route("Home/blog/Update")]
        public ActionResult Update(IEnumerable<Personnel_Records> products)
        {
            try
            {
                var form = Request.Form;
                var df = Request.Form.FirstOrDefault().Value;
                var models = JsonConvert.DeserializeObject<IEnumerable<Personnel_Records>>(Request.Form.FirstOrDefault().Value);
                var fadsf = models;
                var a = 5;
            }
            catch (Exception e)
            {

                throw e;
            }
            return Json(null);
        }
    }
}