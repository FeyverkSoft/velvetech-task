using System;
using System.Text.Json.Serialization;
using Student.Public.Domain.Authentication;

namespace Student.Public.WebApi.Models.Oauth
{
    public class TokenView
    {
        /// <summary>
        /// The access token issued by the authorization server
        /// </summary>
        [JsonPropertyName("access_token")]
        public String AccessToken { get; set; }

        /// <summary>
        /// The type of the token issued. Example: Bearer 
        /// </summary>
        [JsonPropertyName("token_type")]
        public String TokenType { get; set; }

        /// <summary>
        /// The lifetime in seconds of the access token
        /// </summary>
        [JsonPropertyName("expires_in")]
        public Int64 ExpiresIn { get; set; }

        /// <summary>
        /// The refresh token, which can be used to obtain new  access tokens using the same authorization grant
        /// </summary>
        [JsonPropertyName("refresh_token")]
        public String RefreshToken { get; set; }


        internal static TokenView FromToken(Token token)
        {
            return new TokenView
            {
                AccessToken = token.AccessToken,
                RefreshToken = token.RefreshToken,
                TokenType = "Bearer",
                ExpiresIn = (Int64)token.ExpiresIn.TotalSeconds
            };
        }
    }
}
