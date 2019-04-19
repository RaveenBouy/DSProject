using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using DataLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using WebSite.Models;

namespace WebSite.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Header()
        {
            return View();
        }

        public IActionResult Home(AuthResponseModel response)
        {
			ViewData["Response"] = response.Response;
			ViewData["Status"] = response.Status;
			ViewData["Info"] = response.Info;

			return View();
        }

		[HttpGet]
		[Route("AdsPage/viewAdverts/")]
		[Route("AdsPage/viewAdverts/{searchCondition}")]
		[Route("AdsPage/viewAdverts/{searchCondition}/{sortCondition}")]
		[Route("AdsPage/viewAdverts/{searchCondition}/{sortCondition}/{sortDirection}")]
		[Route("AdsPage/viewAdverts/{searchCondition}/{sortCondition}/{sortDirection}/{location}")]
		public ViewResult AdsPage(string searchCondition = "&none&", string sortCondition = "Created", string sortDirection = "DESC", string location = "None")
        {
			AdvertRepository advert = new AdvertRepository();
			List<AdvertisementModel> model = advert.GetAllAdvertisements(searchCondition, sortCondition, sortDirection, location);
			ViewData["Adverts"] = model;

			return View();
        }

		[HttpPost]
		[Route("user/register")]
		public async Task<ActionResult> RegisterAsync(string username, string email, string password)
		{
			UserRepository user = new UserRepository();
			AuthResponseModel response = new AuthResponseModel();
			response = await user.UserRegisterAsync(username, email, password);

			if (response.Response.Equals(200)) {
				return RedirectToAction("AdsPage");
			}
			else
			{
				var rsp = new AuthResponseModel { Response = response.Response, Status = response.Status, Info = response.Info };
				return RedirectToAction("Home", "Home", rsp);
			}

		}

		[HttpGet]
		[Route("user/login")]
		public ActionResult Login(string username, string password)
		{
			UserRepository user = new UserRepository();
			AuthResponseModel response = user.UserLogin(username, password);


			if (response.Response.Equals(200))
			{
				//1st line doesnt work
				return RedirectToAction("AdsPage");
			}
			else
			{
				//1st line doesnt work
				var rsp = new AuthResponseModel { Response = response.Response, Status = response.Status, Info = response.Info };
				return RedirectToAction("Home", "Home", rsp);
			}

		}



		[Route("AdView/viewAdvert/{id}")]
		public ViewResult AdView(int id)
		{
			AdvertRepository advert = new AdvertRepository();
			AdvertisementModel model = advert.GetAdvertisement(id);
			ViewData["Adverts"] = model;
			return View();
		}

        public IActionResult PostAd()
        {
            return View();
        }

		public ViewResult AdTest()
		{
			AdvertRepository advert = new AdvertRepository();
			List<AdvertisementModel> model = advert.GetAllAdvertisements();
			ViewData["Adverts"] = model;
			return View();
		}

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
