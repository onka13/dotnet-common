using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Amazon;
using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using DotNetCommon.AmazonBusiness.Models;
using DotNetCommon.Data.Domain.Models;

namespace DotNetCommon.AmazonBusiness.helpers;

public class AmazonCognitoManager
{
    private AmazonCognitoIdentityProviderClient client;

    public AmazonCognitoManager(AmazonConfig config)
    {
        Config = config;
        var region = RegionEndpoint.GetBySystemName(config.Region);

        client = new AmazonCognitoIdentityProviderClient(Config.AccessKey, Config.SecretKey, new AmazonCognitoIdentityProviderConfig
        {
            RegionEndpoint = region,
        });
    }

    public AmazonConfig Config { get; private set; }

    public async Task<SignInResponse> SignIn(string username, string password)
    {
        try
        {
            var signUpRequest = new SignUpRequest
            {
                ClientId = Config.ClientId,
                Password = password,
                Username = username,
            };

            var emailAttribute = new AttributeType
            {
                Name = "email",
                Value = username,
            };
            signUpRequest.UserAttributes.Add(emailAttribute);

            var response = await client.SignUpAsync(signUpRequest);
            return new SignInResponse
            {
                UserId = response.UserSub,
            };
        }
        catch (Exception ex)
        {
            return new SignInResponse
            {
                ErrorMessage = ex.Message,
            };
        }
    }

    public async Task<AuthResponse> Login(string username, string password)
    {
        try
        {
            var authReq = new AdminInitiateAuthRequest
            {
                UserPoolId = Config.UserPoolId,
                ClientId = Config.ClientId,
                AuthFlow = AuthFlowType.ADMIN_NO_SRP_AUTH,
            };
            authReq.AuthParameters.Add("USERNAME", username);
            authReq.AuthParameters.Add("PASSWORD", password);

            // authReq.AuthParameters.Add("SECRET_HASH", AuthHelper.HashHmac(Config.UserPoolId, Config.ClientSecret));

            AdminInitiateAuthResponse authResp = await client.AdminInitiateAuthAsync(authReq);
            return new AuthResponse
            {
                AccessToken = authResp.AuthenticationResult.AccessToken,
                ExpiresIn = authResp.AuthenticationResult.ExpiresIn,
                IdToken = authResp.AuthenticationResult.IdToken,
                RefreshToken = authResp.AuthenticationResult.RefreshToken,
                TokenType = authResp.AuthenticationResult.TokenType,
            };
        }
        catch (Exception ex)
        {
            return new AuthResponse
            {
                ErrorMessage = ex.Message,
            };
        }
    }

    public async Task<PasswordResponse> ChangePassword(string accessToken, string oldPassword, string newPassword)
    {
        try
        {
            var request = new ChangePasswordRequest
            {
                AccessToken = accessToken,
                PreviousPassword = oldPassword,
                ProposedPassword = newPassword,
            };

            var response = await client.ChangePasswordAsync(request);
            return new PasswordResponse
            {
                IsSuccess = response.HttpStatusCode == System.Net.HttpStatusCode.OK,
            };
        }
        catch (Exception ex)
        {
            return new PasswordResponse
            {
                ErrorMessage = ex.Message,
            };
        }
    }

    public string ValidateToken(string token)
    {
        if (string.IsNullOrWhiteSpace(token))
        {
            return null;
        }

        try
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            if (jwtToken != null && jwtToken.ValidTo > DateTime.UtcNow)
            {
                var clientId = jwtToken.Claims.FirstOrDefault(x => x.Type == "client_id")?.Value;
                if (clientId != Config.ClientId)
                {
                    return null;
                }

                return jwtToken.Subject;
            }

            return null;
        }
        catch
        {
            return null;
        }
    }

    private static byte[] Base64UrlDecode(string input)
    {
        var output = input;
        output = output.Replace('-', '+'); // 62nd char of encoding
        output = output.Replace('_', '/'); // 63rd char of encoding
        // Pad with trailing '='s
        switch (output.Length % 4)
        {
            case 0: break; // No pad chars in this case
            case 1: output += "==="; break; // Three pad chars
            case 2: output += "=="; break; // Two pad chars
            case 3: output += "="; break; // One pad char
            default: throw new Exception("Illegal base64url string!");
        }

        var converted = Convert.FromBase64String(output); // Standard base64 decoder
        return converted;
    }
}
