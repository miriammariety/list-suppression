using API;
using System.Web.Mvc;

namespace TestAPI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Test()
        {
            ApiService service = new ApiService();
            ViewBag.Message = service.GetResponse();
            
            var json = service.ConvertToJson();
            ViewBag.Json = json;

            service.ConvertToCSV(json);

            return View();
        }
    }
}