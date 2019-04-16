﻿using DataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;
using static DataLibrary.BusinessLogic.Validator;
using static DataLibrary.BusinessLogic.Verifier;

namespace DataLibrary.BusinessLogic
{
    public class InsertLibraryItemLogic
    {
        private bool IsVerified { get; set; }
        private bool IsValidated { get; set; }

        public AuthResponseModel SetLibraryItem(ItemModel itemModel, string category)
        {
            var validateResponse = SetResponse(ValidateInput(itemModel));

            if (IsValidated)
            {
                var result = VerifyInsertLibraryItem(itemModel, category);
                var verifyResponse = SetResponse(result);

                if (IsVerified)
                {
                    return SetResponse(10);
                }

                return verifyResponse;
            }
            else
            {
                return validateResponse;
            }
        }

        private int ValidateInput(ItemModel itemModel)
        {
            return ValidateSetLibraryItem(itemModel);
        }

        private AuthResponseModel SetResponse(int decision)
        {
            var jsonResponse = new AuthResponseModel();

            switch (decision)
            {
                case -1:
                    jsonResponse.Response = 401;
                    jsonResponse.Status = "Error";
                    jsonResponse.Info = "No permission to complete this action.";
                    break;
                case 0:
                    jsonResponse.Response = 403;
                    jsonResponse.Status = "Error";
                    jsonResponse.Info = "Could not complete this action. Contact Administrator.";
                    break;
                case 1:
                    IsVerified = true;
                    break;
                case 2:
                    IsValidated = true;
                    break;
                case 3:
                    jsonResponse.Response = 500;
                    jsonResponse.Status = "Error";
                    jsonResponse.Info = "Internal server error";
                    break;
                case 4:
                    jsonResponse.Response = 403;
                    jsonResponse.Status = "Error";
                    jsonResponse.Info = "Auth Token is required to complete this action.";
                    break;
                case 5:
                    jsonResponse.Response = 403;
                    jsonResponse.Status = "Error";
                    jsonResponse.Info = "<Title> is required";
                    break;
                case 6:
                    jsonResponse.Response = 403;
                    jsonResponse.Status = "Error";
                    jsonResponse.Info = "<Description> is required";
                    break;
                case 7:
                    jsonResponse.Response = 403;
                    jsonResponse.Status = "Error";
                    jsonResponse.Info = "<Author> is required";
                    break;
                case 8:
                    jsonResponse.Response = 403;
                    jsonResponse.Status = "Error";
                    jsonResponse.Info = "<PublishYear> is reqired";
                    break;
                case 9:
                    jsonResponse.Response = 403;
                    jsonResponse.Status = "Error";
                    jsonResponse.Info = "<Access> is reqired";
                    break;
                case 10:
                    jsonResponse.Response = 200;
                    jsonResponse.Status = "Success";
                    jsonResponse.Info = "Item added successfully!";
                    break;
            }

            return jsonResponse;
        }
    }
}
