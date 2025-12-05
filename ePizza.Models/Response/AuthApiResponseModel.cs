using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePizza.Models.Response
{
     public class AuthApiResponseModel
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }

        public int TokenExpiryInSeconds { get; set; }
    }

    public class RefreshTokenRequest
    {
        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }

    }
}
