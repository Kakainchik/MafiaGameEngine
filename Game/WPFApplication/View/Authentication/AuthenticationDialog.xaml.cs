using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using WPFApplication.Properties;
using WPFApplication.Resources;

namespace WPFApplication.View
{
    /// <summary>
    /// Interaction logic for AuthenticationWindow.xaml
    /// </summary>
    public partial class AuthenticationDialog : Window
    {
        private HttpClient client;
        private CookieContainer cookies;
        private bool isLoginMode = true;

        public AuthenticationDialog(HttpClient client, CookieContainer cookies)
        {
            this.client = client;
            this.cookies = cookies;

            InitializeComponent();
        }

        private async Task Login()
        {
            LoginRequest request = new(usernameTextBox.Text, passwordBox.Password);
            var response = await client.PostAsJsonAsync<LoginRequest>("/api/auth/login", request);

            if(response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsAsync<LoginResponse>();
                var rtoken = cookies.GetAllCookies()["RefreshToken"]?.Value;

                Settings.Default.ApiId = content.Id;
                Settings.Default.ApiUsername = content.Username;
                Settings.Default.JwtToken = content.JwtToken;
                Settings.Default.RefreshToken = rtoken;
                Settings.Default.Save();

                ShowMessage(ErrorResources.SignInSuccess);
                DialogResult = true;
            }
            else ShowError(ErrorResources.BadSignIn);
        }

        private async Task Register()
        {
            RegisterRequest request = new(usernameTextBox.Text, passwordBox.Password);
            var response = await client.PostAsJsonAsync("/api/auth/signup", request);

            if(response.IsSuccessStatusCode)
            {
                ShowMessage(ErrorResources.SignUpSuccess);
                signInRB.IsChecked = true;
            }
            else ShowError(ErrorResources.BadSignUp);
        }

        private void ShowError(string message)
        {
            responseTextBlock.Text = message;
            responseTextBlock.Foreground = Brushes.PaleVioletRed;
        }

        private void ShowMessage(string message)
        {
            responseTextBlock.Text = message;
            responseTextBlock.Foreground = Brushes.Green;
        }

        private async void Login_Click(object sender, RoutedEventArgs e)
        {
            this.IsEnabled = false;

            if(isLoginMode) await Login();
            else await Register();

            usernameTextBox.ClearValue(TextBox.TextProperty);
            passwordBox.Clear();
            this.IsEnabled = true;
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if(sender == signInRB)
            {
                isLoginMode = true;
                mainButton.Content = "Login";
            }
            else if(sender == signUpRB)
            {
                isLoginMode = false;
                mainButton.Content = "Sign Up";
            }
        }

        private record class LoginRequest(string Username, string Password);
        private record class LoginResponse(long Id,
            string Username,
            string JwtToken);

        private record class RegisterRequest(string Username, string Password);
    }
}