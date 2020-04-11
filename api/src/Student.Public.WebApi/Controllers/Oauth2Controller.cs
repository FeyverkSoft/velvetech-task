using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Student.Public.Domain.Authentication;
using Student.Public.WebApi.Models.Oauth;

namespace Student.Public.WebApi.Controllers
{
    [ApiController]
    [AllowAnonymous]
    public class Oauth2Controller : ControllerBase
    {
        /// <summary>
        /// User authentication
        /// </summary>
        /// <param name="grantType">(password, refresh_token)</param>
        /// <param name="userName">login</param>
        /// <param name="password">Password</param>
        /// <param name="refreshToken">Refresh token</param>
        /// <response code="200">Successfully</response>
        /// <response code="400">Bad request</response>
        [HttpPost("oauth2/token")]
        [ProducesResponseType(200, Type = typeof(TokenView))]
        [ProducesResponseType(400, Type = typeof(ErrorView))]
        public async Task<IActionResult> Auth(
            CancellationToken cancellationToken,
            [FromForm(Name = "grant_type")] String grantType,
            [FromForm(Name = "username")] String userName,
            [FromForm(Name = "password")] String password,
            [FromForm(Name = "refresh_token")] String refreshToken,
            [FromServices] UserAuthenticationService authenticationService)
        {
            // много дичи согласно RFC
            const String passwordGrantType = "password";
            const String refreshTokenGrantType = "refresh_token";

            if (String.IsNullOrEmpty(grantType))
                return BadRequest(ErrorView.Build(ErrorCode.InvalidRequest, "Field 'grant_type' is required"));

            switch (grantType){
                case passwordGrantType:
                    if (String.IsNullOrEmpty(userName))
                        BadRequest(ErrorView.Build(ErrorCode.InvalidRequest,
                            $"Field 'username' is required for '{passwordGrantType}' grant type"));

                    if (String.IsNullOrEmpty(password))
                        BadRequest(ErrorView.Build(ErrorCode.InvalidRequest,
                            $"Field 'password' is required for '{passwordGrantType}' grant type"));

                    try{
                        return Ok(TokenView.FromToken(
                            await authenticationService.AuthenticationByPassword(userName, password,
                                cancellationToken)));
                    }
                    catch (UnauthorizedException){
                        return BadRequest(ErrorView.Build(ErrorCode.UnauthorizedClient,
                            "Login or password is incorrect"));
                    }
                    catch (UnconfirmedException){
                        return BadRequest(ErrorView.Build(ErrorCode.InvalidClient, "Registration is unconfirmed"));
                    }

                case refreshTokenGrantType:
                    if (String.IsNullOrEmpty(refreshToken))
                        BadRequest(ErrorView.Build(ErrorCode.InvalidRequest,
                            $"Field 'refresh_token' is required for '{refreshTokenGrantType}' grant type"));

                    try{
                        return Ok(TokenView.FromToken(
                            await authenticationService.AuthenticationByRefreshToken(refreshToken, cancellationToken)));
                    }
                    catch (UnauthorizedException){
                        return BadRequest(ErrorView.Build(ErrorCode.UnauthorizedClient, "Refresh token is incorrect"));
                    }
                    catch (UnconfirmedException){
                        return BadRequest(ErrorView.Build(ErrorCode.InvalidClient, "Registration is unconfirmed"));
                    }

                default:
                    return BadRequest(ErrorView.Build(ErrorCode.UnsupportedGrantType,
                        $"Unsupported grant type: {grantType}. Possible types: {passwordGrantType}, {refreshTokenGrantType}"));
            }
        }
    }
}