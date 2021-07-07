using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;
using Bilbayt.WebClient.Models;
using Bilbayt.WebClient.Models.ResponseErrorModels;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;

namespace Bilbayt.WebClient.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private readonly ILocalStorageService _localStorage;

        public AuthService(HttpClient httpClient,
                           AuthenticationStateProvider authenticationStateProvider,
                           ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _authenticationStateProvider = authenticationStateProvider;
            _localStorage = localStorage;
        }

        public async Task<CreateUserResult> Register(CreateUserModel createUserModel)
        {
            try
            {
                var userAsJson = JsonSerializer.Serialize(createUserModel);

                var response = await _httpClient.PostAsync(Constants.ApiUrlConstants.RegisterUrl, new StringContent(userAsJson, Encoding.UTF8, "application/json"));

                var userId = await response.Content.ReadAsStringAsync();

                if (string.IsNullOrEmpty(userId))
                    return new CreateUserResult()
                    {
                        IsSuccess = false,
                        ErrorMessage = "Registration response can not be read"
                    };

                var registerResult = new CreateUserResult { Id = userId, IsSuccess = response.IsSuccessStatusCode };

                if (!response.IsSuccessStatusCode)
                {
                    switch (response.StatusCode)
                    {
                        case HttpStatusCode.BadRequest:

                            var responseObject = JsonSerializer.Deserialize<CreateUserResponseError>(await response.Content.ReadAsStringAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                            var errorBuilder = new StringBuilder("Please fix the following issue: ");

                            foreach (var pair in responseObject.Errors)
                            {
                                foreach (var er in pair.Value)
                                {
                                    errorBuilder.AppendLine(er);
                                }
                            }
                            registerResult.ErrorMessage = errorBuilder.ToString();

                            break;

                        case HttpStatusCode.InternalServerError:
                        case HttpStatusCode.UnsupportedMediaType:
                            registerResult.ErrorMessage = "Internal error. Please, try again later";
                            break;

                        case HttpStatusCode.ServiceUnavailable:
                        case HttpStatusCode.RequestTimeout:
                            registerResult.ErrorMessage = "Problem with connecting to server. Please, try again later ";
                            break;

                        default:
                            var error = JsonSerializer.Deserialize<dynamic>(response.Content.ReadAsStringAsync().Result);
                            registerResult.ErrorMessage = $"{response.StatusCode} {response.ReasonPhrase}: {error}";
                            break;
                    }
                }

                return registerResult;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Exception: {e}");
                return new CreateUserResult()
                {
                    IsSuccess = false,
                    ErrorMessage = e.Message
                };
            }
        }

        public async Task<UserProfileResult> GetProfile(string id)
        {
            try
            {
                var uri = $"{Constants.ApiUrlConstants.ProfileUrl}/{HttpUtility.UrlEncode(id)}";

                var response = await _httpClient.GetAsync(uri);

                var profile = JsonSerializer.Deserialize<UserProfileResult>(await response.Content.ReadAsStringAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                if (profile == null)
                    return new UserProfileResult()
                    {
                        IsSuccess = false,
                        ErrorMessage = "Authentication response can not be read"
                    };

                profile.IsSuccess = response.IsSuccessStatusCode;
                if (!response.IsSuccessStatusCode)
                {
                    PrepareErrorMessage(response, profile);
                }

                profile.ErrorMessage = response.ReasonPhrase;
                return profile;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new UserProfileResult()
                {
                    IsSuccess = false,
                    ErrorMessage = e.Message
                };
            }
        }
        

        public async Task<AuthenticateResult> Login(AuthenticateModel authenticateModel)
        {
            try
            {
                var loginAsJson = JsonSerializer.Serialize(authenticateModel);
                var response = await _httpClient.PostAsync(Constants.ApiUrlConstants.LoginUrl, new StringContent(loginAsJson, Encoding.UTF8, "application/json"));
                var loginResult = JsonSerializer.Deserialize<AuthenticateResult>(await response.Content.ReadAsStringAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if (loginResult == null)
                    return new AuthenticateResult()
                    {
                        IsSuccess = false,
                        ErrorMessage = "Authentication response can not be read"
                    };

                loginResult.IsSuccess = response.IsSuccessStatusCode;

                if (!response.IsSuccessStatusCode)
                {
                    PrepareErrorMessage(response, loginResult);
                    return loginResult;
                }

                await _localStorage.SetItemAsync("authToken", loginResult.Token);
                ((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsAuthenticated(authenticateModel.Username);
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", loginResult.Token);

                return loginResult;
            }
            catch (Exception e)
            {
                return new AuthenticateResult()
                {
                    IsSuccess = false,
                    ErrorMessage = e.Message
                };
            }
        }

        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync("authToken");
            ((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsLoggedOut();
            _httpClient.DefaultRequestHeaders.Authorization = null;
        }

        private static void PrepareErrorMessage(HttpResponseMessage response, BaseResultModel result)
        {
            switch (response.StatusCode)
            {
                case HttpStatusCode.InternalServerError:
                case HttpStatusCode.UnsupportedMediaType:
                case HttpStatusCode.BadRequest:
                    result.ErrorMessage = "Internal error. Please, try again later";
                    break;
                case HttpStatusCode.NonAuthoritativeInformation:
                case HttpStatusCode.Unauthorized:
                    result.ErrorMessage = "Email or password incorrect!";
                    break;

                case HttpStatusCode.ServiceUnavailable:
                case HttpStatusCode.RequestTimeout:
                    result.ErrorMessage = "Problem with connecting to server. Please, try again later ";
                    break;
                default:
                    var error = JsonSerializer.Deserialize<dynamic>(response.Content.ReadAsStringAsync().Result);
                    result.ErrorMessage = $"{response.StatusCode} {response.ReasonPhrase}: {error}";
                    break;
            }
        }
    }
}
