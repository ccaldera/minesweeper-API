using Newtonsoft.Json;
namespace CC.Minesweeper.Api.Controllers.Token.Models
{
    public class LoginResponse
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("token_type")]
        public string TokenType { get; set; } = ServiceConstants.TokenType;
    }
}
