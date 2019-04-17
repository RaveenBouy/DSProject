using LitJson;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace WebSite.Models
{
	public class AdvertRepository : IAdRepository
	{
		private List<AdvertisementModel> _advertList = new List<AdvertisementModel>();

		public AdvertRepository()
		{

		}

		public AdvertisementModel GetAdvertisement(int id)
		{
			throw new NotImplementedException();
		}

		public List<AdvertisementModel> GetAllAdvertisements()
		{
			try
			{
				using (var streamReader = new StreamReader(((HttpWebResponse)((HttpWebRequest)WebRequest.Create(
				string.Concat(new string[]
				{
					"https://localhost:44376/api/ad/viewAdverts/"

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
