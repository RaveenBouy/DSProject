﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using DataLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using WebSite.Models;
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.Http;

namespace WebSite.Controllers
{
    public class HomeController : Controller
    {

		const string SessionName = "_Username";


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

			ViewBag.Name = HttpContext.Session.GetString(SessionName);


			return View();
        }



		[HttpGet]
		[Route("AdsPage/viewAdverts/")]
		[Route("AdsPage/viewAdverts/{searchCondition}")]
		[Route("AdsPage/viewAdverts/{searchCondition}/{sortCondition}")]
		[Route("AdsPage/viewAdverts/{searchCondition}/{sortCondition}/{sortDirection}")]
		[Route("AdsPage/viewAdverts/{searchCondition}/{sortCondition}/{sortDirection}/{location}")]
		public ViewResult AdsPage(string searchCondition = "&none&", string sortCondition = "created", string sortDirection = "desc", string location = "none")
        {
			AdvertRepository advert = new AdvertRepository();
			List<AdvertisementModel> model = advert.GetAllAdvertisements(searchCondition, sortCondition, sortDirection, location);
			ViewData["Adverts"] = model;

			ViewBag.Name = HttpContext.Session.GetString(SessionName);
		//	ViewBag.Age = HttpContext.Session.GetInt32(SessionAge);

			return View();
        }


		[Route("AdView/viewAdvert/{id}")]
		public ViewResult AdView(int id)
		{
			AdvertRepository advert = new AdvertRepository();
			AdvertisementModel model = advert.GetAdvertisement(id);
			ViewData["Adverts"] = model;
			return View();
		}


		[Route("AdsPage/postad/")]
		public IActionResult PostAd()
		{
			return View();
		}


		[HttpPost]
		[Route("user/register")]
		public async Task<ActionResult> RegisterAsync(string username, string email, string password)
		{
			UserRepository user = new UserRepository();
			AuthResponseModel response = new AuthResponseModel();
			response = await user.UserRegisterAsync(username, email, password);

			if (response.Response.Equals(200))
			{
				return RedirectToAction("AdsPage");
			}
			else
			{
				var rsp = new AuthResponseModel { Response = response.Response, Status = response.Status, Info = response.Info };
				return RedirectToAction("Home", "Home", rsp);
			}

		}


		[HttpPost]
		[Route("ad/postAd/")]
		public async Task<ActionResult> postAd(string iName, string iCategory, string iAvailable, string iLoc, string iDescription, string iContact, string iPrice, string iCondition, string iNegotiable)
		{
			AdvertRepository ad = new AdvertRepository();
			AuthResponseModel response = new AuthResponseModel();

			int negotiable = 0;

			if (iNegotiable.Equals("Negotiable"))
			{
				negotiable = 1;
			}
			else {
				negotiable = 0;
			}

			response = await ad.AddAdvert(iName, iCategory, iAvailable, iLoc, iDescription, iContact, iPrice,iCondition, negotiable);

			if (response.Response.Equals(200))
			{
				return RedirectToAction("AdsPage");
			}
			else
			{
				var rsp = new AuthResponseModel { Response = response.Response, Status = response.Status, Info = response.Info };
				return RedirectToAction("Home", "Home");
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
				HttpContext.Session.SetString(SessionName, username);
				return RedirectToAction("AdsPage");
			}
			else
			{
				//1st line doesnt work
				var rsp = new AuthResponseModel { Response = response.Response, Status = response.Status, Info = response.Info };
				return RedirectToAction("Home", "Home", rsp);
			}

		}


		[HttpGet]
		[Route("user/logout")]
		public async Task<IActionResult> Logout()
		{
			HttpContext.Session.Remove(SessionName);
			return RedirectToAction("Home", "Home");
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
