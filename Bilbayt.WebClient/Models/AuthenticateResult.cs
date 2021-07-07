namespace Bilbayt.WebClient.Models
{
    public class AuthenticateResult : BaseResultModel
    {
        public string Id { get; set; }
        public string Token { get; set; }
    }
}
