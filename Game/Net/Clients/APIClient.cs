using Microsoft.AspNetCore.WebUtilities;
using Net.Models.APIModels;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace Net.Clients
{
    public class APIClient : IDisposable
    {
        private const string API_URL = @"https://localhost:7262/api";
        private const string HALL_URL = $"{API_URL}/hall";
        private const string LOGIN_URL = $"{API_URL}/auth/login";
        private const string SIGNUP_URL = $"{API_URL}/auth/signup";
        private const string AUTHENTICATE_URL = $"{API_URL}/auth/refresh-token";
        private const string JSON_MIME = @"application/json";
        private const long BUFFER_SIZE = 1024 * 64;

        private HttpClient httpClient;
        private CookieContainer cookieContainer;
        private HttpClientHandler httpHandler;
        private bool disposedValue;

        public AccountDomain? Account { get; private set; }

        private string? RefreshToken => cookieContainer.GetAllCookies()["RefreshToken"]?.Value;

        public APIClient(ProductInfoHeaderValue productInfo)
        {
            cookieContainer = new CookieContainer();
            httpHandler = new HttpClientHandler()
            {
                UseCookies = true,
                CookieContainer = cookieContainer
            };
            httpClient = new HttpClient(httpHandler, true)
            {
                BaseAddress = new Uri(HALL_URL),
                MaxResponseContentBufferSize = BUFFER_SIZE
            };
            httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue(JSON_MIME));
            httpClient.DefaultRequestHeaders.UserAgent.Add(productInfo);
        }

        public async Task<AccountDomain?> LoginAsync(LoginRequest req)
        {
            var response = await httpClient.PostAsJsonAsync<LoginRequest>(LOGIN_URL, req);

            if(response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsAsync<AuthenticateResponse>();
                Account = new AccountDomain(content.Username,
                    content.JwtToken,
                    RefreshToken!);

                httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", content.JwtToken);
            }
            return Account;
        }

        /// <summary>
        /// Get a new JWT token by using the refresh token.
        /// </summary>
        /// <returns></returns>
        public async Task<bool> AuthenticateAsync()
        {
            if(RefreshToken == null) return false;

            var query = QueryHelpers.AddQueryString(AUTHENTICATE_URL, "tkn", RefreshToken);
            var response = await httpClient.PostAsync(query, null);

            if(response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsAsync<AuthenticateResponse>();
                Account = new AccountDomain(content.Username,
                    content.JwtToken,
                    RefreshToken!);
                httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", content.JwtToken);
            }
            return response.IsSuccessStatusCode;
        }

        #region IDisposable implementation
#nullable disable warnings

        protected virtual void Dispose(bool disposing)
        {
            if(!disposedValue)
            {
                if(disposing)
                {
                    //Dispose managed state (managed objects)
                }

                //Set large fields to null
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

#nullable restore warnings
        #endregion
    }
}