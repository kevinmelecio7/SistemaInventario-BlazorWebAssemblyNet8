namespace AppInvWebClient.ClientServices
{
    public class TokenService
    {
        public string JWTToken { get; private set; } = string.Empty;

        public void SetToken(string token)
        {
            JWTToken = token;
        }

        public void ClearToken()
        {
            JWTToken = string.Empty;
        }
    }
}
