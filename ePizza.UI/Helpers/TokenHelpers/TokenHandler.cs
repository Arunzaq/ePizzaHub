

using System.Net.Http.Headers;

namespace ePizza.UI.Helpers.TokenHelpers
{
    public class TokenHandler : DelegatingHandler
    {
        private readonly ITokenServices _tokenServices;

        public TokenHandler(ITokenServices tokenServices) 
        {
            _tokenServices = tokenServices;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {

            var token = _tokenServices.GetToken();

            // If the token is available, add it to the Authorization header
            if (!string.IsNullOrEmpty(token))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            return base.SendAsync(request, cancellationToken);
        }
    }
}
