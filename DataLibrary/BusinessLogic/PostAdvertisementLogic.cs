using DataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;


namespace DataLibrary.BusinessLogic
{
	public class PostAdvertisementLogic
	{
		private bool IsVerified { get; set; }

		public AuthResponseModel PostAd(AdvertisementModel adModel)
		{
			var result = AdvertisementProcessor.AddPost(adModel);
			var verifyResponse = SetResponse(result);

			if (IsVerified)
			{
				return SetResponse(5);
			}
			else
			{
				return verifyResponse;
			}
		}

		private AuthResponseModel SetResponse(int decision)
		{
			var jsonResponse = new AuthResponseModel();

			switch (decision)
			{
				case 0:
					jsonResponse.Response = 403;
					jsonResponse.Status = "Error";
					jsonResponse.Info = "Could not complete this action. Contact Administrator.";
					break;
				case 1:
					IsVerified = true;
					break;
				case 3:
					jsonResponse.Response = 500;
					jsonResponse.Status = "Error";
					jsonResponse.Info = "Internal server error";
					break;
				case 5:
					jsonResponse.Response = 200;
					jsonResponse.Status = "Success";
					jsonResponse.Info = "Item added successfully!";
					break;
			}

			return jsonResponse;
		}


		

	}
}
