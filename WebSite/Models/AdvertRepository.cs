using DataLibrary.Models;
using LitJson;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebSite.Models
{
	public class AdvertRepository
	{
		private List<AdvertisementModel> _advertList = new List<AdvertisementModel>();

		public AdvertRepository()
		{

		}


		public async Task<AuthResponseModel> AddAdvert(string iName, string iCategory, string iAvailable, string iLoc, string iDescription, string iContact, string iPrice, string iCondition, int iNegotiable)
		{

			string myJason = "{'ItemName': '" + iName + "', 'ItemDescription': '" + iDescription + "', 'ItemCategory': '" + iCategory+ "', 'ItemCount': '" + iAvailable + "', 'City': '" + iLoc + "', 'Tele': '" + iContact + "', 'Price': '" + iPrice + "', 'Con': '" + iCondition + "', 'Negotiable': " + iNegotiable+ "}";

			JsonData json = new JsonData();
			var response = new AuthResponseModel();

			using (var client = new HttpClient())
			{
				var post = await client.PostAsync(
					"https://localhost:44376/api/ad/advertisement",
					new StringContent(myJason, System.Text.Encoding.UTF8, "application/json"));
				json = await post.Content.ReadAsStringAsync();
				json.ToString();
			}

			try
			{
				json = JsonMapper.ToObject(json.ToString());
				response.Response = (int)json["response"];
				response.Status = json["status"].ToString();
				response.Info = json["info"].ToString();
			}
			catch (Exception e)
			{
				Console.WriteLine(e.GetBaseException().ToString());
			}

			return response;
		}





		public AdvertisementModel GetAdvertisement(int id)
		{
			AdvertisementModel adModel = null;
			try
			{
				using (var streamReader = new StreamReader(((HttpWebResponse)((HttpWebRequest)WebRequest.Create(
				string.Concat(new string[]
				{
				$"https://localhost:44376/api/ad/viewPostById/{id}"

				}))).GetResponse()).GetResponseStream()))
				{
					var json = JsonMapper.ToObject(streamReader.ReadToEnd());

					adModel = new AdvertisementModel
					{
						ItemId = json[0]["itemId"].ToString(),
						ItemName = json[0]["itemName"].ToString(),
						ItemDescription = json[0]["itemDescription"].ToString(),
						ItemCount = json[0]["itemCount"].ToString(),
						City = json[0]["city"].ToString(),
						ItemCategory = json[0]["itemCategory"].ToString(),
						Tele = json[0]["tele"].ToString(),
						Price = json[0]["price"].ToString(),
						Con = json[0]["con"].ToString(),
						Negotiable = json[0]["negotiable"].ToString(),
						Created = json[0]["created"].ToString(),
						Updated = json[0]["updated"].ToString(),
						ViewCount = json[0]["viewCount"].ToString()
					};


				}
			}
			catch (Exception)
			{

				throw;
			}

			return adModel;
		}



		public List<AdvertisementModel> GetAllAdvertisements(string searchCondition = "&none&", string sortCondition = "created", string sortDirection = "desc", string location = "none")
		{
			try
			{
				using (var streamReader = new StreamReader(((HttpWebResponse)((HttpWebRequest)WebRequest.Create(
				string.Concat(new string[]
				{
			$"https://localhost:44376/api/ad/viewAdverts/{searchCondition}/{sortCondition}/{sortDirection}/{location}"

				}))).GetResponse()).GetResponseStream()))
				{
					var json = JsonMapper.ToObject(streamReader.ReadToEnd());

					for (int i = 0; i < json.Count; i++)
					{
						_advertList.Add(new AdvertisementModel
						{
							ItemId = json[i]["itemId"].ToString(),
							ItemName = json[i]["itemName"].ToString(),
							ItemDescription = json[i]["itemDescription"].ToString(),
							ItemCount = json[i]["itemCount"].ToString(),
							City = json[i]["city"].ToString(),
							ItemCategory = json[i]["itemCategory"].ToString(),
							Tele = json[i]["tele"].ToString(),
							Price = json[i]["price"].ToString(),
							Con = json[i]["con"].ToString(),
							Negotiable = json[i]["negotiable"].ToString(),
							Created = json[i]["created"].ToString(),
							Updated = json[i]["updated"].ToString(),
							ViewCount = json[i]["viewCount"].ToString()

						});
					}

				}
			}
			catch (Exception)
			{

				throw;
			}
			return _advertList;
		}
	}
}
