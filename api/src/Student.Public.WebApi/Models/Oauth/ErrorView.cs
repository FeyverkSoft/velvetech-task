using System;
using System.Text.Json.Serialization;

namespace Student.Public.WebApi.Models.Oauth
{
    public sealed class ErrorView
    {
        /// <summary>
        /// Error code https://tools.ietf.org/html/rfc6749#section-5.2
        /// </summary>
        [JsonPropertyName("error")]
        public String Error { get; private set; }

        /// <summary>
        /// Human-readable text providing additional information
        /// </summary>
        [JsonPropertyName("error_description")]
        public String ErrorDescription { get; private set; }

        public static ErrorView Build(ErrorCode code, String description = null)
        {
            return new ErrorView
            {
                Error = MapErrorCode(code),
                ErrorDescription = description
            };
        }

        private static String MapErrorCode(ErrorCode code)
        {
            return code switch
            {
                ErrorCode.InvalidRequest => "invalid_request",
                ErrorCode.InvalidClient => "invalid_client",
                ErrorCode.InvalidGrant => "invalid_grant",
                ErrorCode.UnauthorizedClient => "unauthorized_client",
                ErrorCode.UnsupportedGrantType => "unsupported_grant_type",
                ErrorCode.InvalidScope => "invalid_scope",
                _ => throw new ArgumentException($"Unknown code {code}", nameof(code))
            };
        }
    }
}