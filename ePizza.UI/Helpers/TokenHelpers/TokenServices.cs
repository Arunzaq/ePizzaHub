namespace ePizza.UI.Helpers.TokenHelpers
{
    public class TokenServices : ITokenServices
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TokenServices(IHttpContextAccessor httpContextAccessor) 
        {
            _httpContextAccessor = httpContextAccessor;
        }


        public string GetToken()
        {
            return _httpContextAccessor.HttpContext.Request.Cookies["AccessToken"];
        }

        public void SetToken(string token)
        {
            _httpContextAccessor.HttpContext.Response.Cookies.Append(
                "AccessToken", token,
                new CookieOptions
                {
                    Expires = DateTime.Now.AddMinutes(60)
                });
        }
    }
}
