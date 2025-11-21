namespace ePizza.UI.Helpers.TokenHelpers
{
    public interface ITokenServices
    {
        void SetToken(string Token);   // save Token In cookies or session storage,local storage
        string GetToken();   /// get Token from cookies
    }
}
