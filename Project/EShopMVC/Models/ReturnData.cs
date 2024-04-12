using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EShopMVC.Models
{
    public class ReturnData
    {
        public int ReturnCode { get; set; }
        public string ReturnMessage { get; set; }
    }

    public class LoginResponseData : ReturnData
    {
        //Chú ý: những thông tin này phải y chang class UserLoginReturnData model ở bên API
        public string userName { get; set; }
        public string token { get; set; }
        public string refreshToken { get; set; }
        public int IsAdmin { get; set; }
    }

}