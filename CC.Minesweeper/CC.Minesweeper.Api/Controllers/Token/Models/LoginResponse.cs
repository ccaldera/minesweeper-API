using Newtonsoft.Json;
namespace CC.Minesweeper.Api.Controllers.Token.Models
{
    /// <summary>
    /// The login response class.
    /// </summary>
    public class LoginResponse
    {
        /// <summary>
        /// Gets or sets the access token.
        /// </summary>
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        /// <summary>
        /// Gets or sets the acces tonen type.
        /// </summary>
        [JsonProperty("token_type")]
        public string TokenType { get; set; } = ServiceConstants.TokenType;
    }
}
