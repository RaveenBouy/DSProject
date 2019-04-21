using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DataLibrary.Models;
using DataLibrary.BusinessLogic;


namespace API.Controllers
{
    public class AdController : Controller
    {
		[HttpPost("api/ad/advertisement")]
		public DataLibrary.Models.AuthResponseModel addPost([FromBody] AdvertisementModel adModel)
		{
			PostAdvertisementLogic postAdvertisement = new PostAdvertisementLogic();
			return postAdvertisement.PostAd(adModel);
		}

		[HttpGet]
		[Route("api/ad/viewAdverts/")]
		[Route("api/ad/viewAdverts/{searchCondition}")]
		[Route("api/ad/viewAdverts/{searchCondition}/{sortCondition}")]
		[Route("api/ad/viewAdverts/{searchCondition}/{sortCondition}/{sortDirection}")]
		[Route("api/ad/viewAdverts/{searchCondition}/{sortCondition}/{sortDirection}/{location}")]
		public IEnumerable<AdvertisementModel> ViewPosts(string searchCondition = "&none&", string sortCondition = "created", string sortDirection = "desc", string location = "none")
		{
			return AdvertisementProcessor.ViewAllPosts(searchCondition ,sortCondition, sortDirection, location);
		}


		[HttpGet("api/ad/viewPostById/{id}")]
		public List<AdvertisementModel> ViewPostById(int id)
		{
			return AdvertisementProcessor.ViewPostById(id);
		}

	}
}