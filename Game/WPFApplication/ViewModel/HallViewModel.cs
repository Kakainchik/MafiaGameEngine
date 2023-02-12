using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Input;
using WPFApplication.Core;
using WPFApplication.Model;
using WPFApplication.Properties;
using WPFApplication.View;

namespace WPFApplication.ViewModel
{
    public class HallViewModel : ChangeablePage, INetHolder
    {
        private const string BASE_URL = "https://localhost:7262";

        private HttpClient httpClient;
        private CookieContainer cookies;
        private bool authenticated;
        private bool isUIEnabled;

        public bool IsUIEnabled
        {
            get => isUIEnabled;
            set
            {
                isUIEnabled = value;
                OnPropertyChanged(nameof(IsUIEnabled));
            }
        }

        public bool Authenticated
        {
            get => authenticated;
            set
            {
                authenticated = value;
                OnPropertyChanged(nameof(Authenticated));
            }
        }

        public INetHolder NetHolder { get; set; }
        public ICommand LoginCommand { get; set; }

        public HallViewModel()
        {
            LoginCommand = new RelayCommand(OnLogin);

            cookies = new CookieContainer();
            using(HttpClientHandler handler = new HttpClientHandler()
            {
                UseCookies = true,
                CookieContainer = cookies
            })
            {
                httpClient = new HttpClient(handler, true)
                {
                    BaseAddress = new Uri(BASE_URL)
                };
            }
            httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.UserAgent.Add(
                new ProductInfoHeaderValue("MafiaGameWindows",
                    Assembly.GetEntryAssembly().GetName().Version.ToString()));

            if(string.IsNullOrEmpty(Settings.Default.JwtToken))
            {
                Authenticated = false;
            }
            else
            {
                IsUIEnabled = false;

                httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", Settings.Default.JwtToken);

                var task = httpClient.GetAsync("/api/hall");
                task.Wait();
                var response = task.Result;

                if(response.IsSuccessStatusCode)
                {
                    Task<HallLobby[]> read = response.Content.ReadAsAsync<HallLobby[]>();
                    read.Wait();
                    //TODO: Show lobbies items
                }
                else
                {
                    //TODO: Get new token
                    //We should have a refresh token in our cookies
                    UriBuilder rfrUri = new UriBuilder(BASE_URL);
                    rfrUri.Path = "/api/refresh-token";
                    rfrUri.Query = $"tnk={Settings.Default.RefreshToken}";
                    httpClient.PostAsync(rfrUri.Uri, null);
                }

                IsUIEnabled = true;
            }
        }

        public void AbortConnections()
        {
            httpClient.Dispose();

            NetHolder?.AbortConnections();
        }

        public override void HandlePageChange(ChangeablePage page)
        {

        }

        private void OnLogin(object obj)
        {
            AuthenticationDialog dialog = new AuthenticationDialog(httpClient, cookies);
            bool dresult = dialog.ShowDialog() == true;
            if(dresult)
            {

            }
        }
    }
}