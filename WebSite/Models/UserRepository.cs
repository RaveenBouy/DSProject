using DataLibrary.Models;
using System;
using System.Threading.Tasks;
using LitJson;
using System.Net.Http;
using System.IO;
using System.Net;

namespace WebSite.Models
{
    public class UserRepository
	{
		public UserRepository()
		{}

		public async Task<AuthResponseModel> UserRegisterAsync(string username, string email, string password)
		{
			string myJason = "{'UserName': '" + username + "', 'Email': '" + email + "', 'Pass': '" + password + "'}";

			JsonData json = new JsonData();
			var response = new AuthResponseModel();
            try
            {
                using (var client = new HttpClient())
			    {
				    var post = await client.PostAsync(
                        "http://localhost:20000/api/register",
					    new StringContent(myJason, System.Text.Encoding.UTF8, "application/json"));
				    json = await post.Content.ReadAsStringAsync();
				    json.ToString();
			    }
			
				json = JsonMapper.ToObject(json.ToString());
				response.Response = (int)json["response"];
				response.Status = json["status"].ToString();
				response.Info = json["info"].ToString();
			}
			catch (Exception e)
			{
                using (var client = new HttpClient())
                {
                    var post = await client.PostAsync(
                        "http://localhost:20000/api/register",
                        new StringContent(myJason, System.Text.Encoding.UTF8, "application/json"));
                    json = await post.Content.ReadAsStringAsync();
                    json.ToString();
                }

                json = JsonMapper.ToObject(json.ToString());
                response.Response = (int)json["response"];
                response.Status = json["status"].ToString();
                response.Info = json["info"].ToString();
            }

			return response;
		}


		public AuthResponseModel UserLogin(string username, string password)
		{
			AuthResponseModel authModel = null;
			try
			{
				using (var streamReader = new StreamReader(((HttpWebResponse)((HttpWebRequest)WebRequest.Create(
				string.Concat(new string[]
				{
				$"http://localhost:20000/api/login?username={username}&password={password}"
				}))).GetResponse()).GetResponseStream()))
				{
					var json = JsonMapper.ToObject(streamReader.ReadToEnd());

					authModel = new AuthResponseModel
					{
						Response = (int)json["response"],
						Status = json["status"].ToString(),
						Token = json["token"].ToString()
					};
				}
			}
			catch (Exception)
			{
                using (var streamReader = new StreamReader(((HttpWebResponse)((HttpWebRequest)WebRequest.Create(
                string.Concat(new string[]
                {
                $"http://localhost:20000/api/login?username={username}&password={password}"
                }))).GetResponse()).GetResponseStream()))
                {
                    var json = JsonMapper.ToObject(streamReader.ReadToEnd());

                    authModel = new AuthResponseModel
                    {
                        Response = (int)json["response"],
                        Status = json["status"].ToString(),
                        Token = json["token"].ToString()
                    };
                }
            }

			return authModel;
		}
	}
}
