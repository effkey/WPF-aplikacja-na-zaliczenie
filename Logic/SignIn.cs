using Microsoft.VisualBasic;
using Projekt_WPF_TODO_app.Logic.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Projekt_WPF_TODO_app.Logic
{
    public class SignIn
    {
        public User user { get; set; }

        public string NewUserPassword { get; set; }
        public string NewUserPassword2 { get; set; }
        public string NewUserEmail { get; set; }
        public string NewUserName { get; set; }
        public string NewUserToken { get; set; }
        public bool IsSignInSuccess { get; set; }
        public string? Response { get; set; }
        public string ErrorResponse { get; set; }


        public event EventHandler<bool>? SignInCompleted;
        public ICommand? SignInCommend { get; set; }

        public SignIn(User user)
        {
            this.user = user;
            SignInCommend = new RelayCommand(SignInIntoApp);
        }
        public void SignInIntoApp()
        {
            user.Username = NewUserName;
            user.Password = NewUserPassword;
            user.Password2 = NewUserPassword2;
            user.Email = NewUserEmail;

            var signInUserData = new
            {
                username = user.Username,
                email = user.Email,
                password = user.Password,
                password2 = user.Password2,
            };

            var signInHandler = new ApiHelper("http://kubpi.pythonanywhere.com/");
            var signInDataInJson = JsonSerializer.Serialize(signInUserData);
            var signInResponse = signInHandler.SendPostRequest(signInDataInJson, "register");
            Response = signInResponse;

            try
            {
                var responseJsonDocument = JsonDocument.Parse(signInResponse);

                if (responseJsonDocument.RootElement.TryGetProperty("username", out var usernameProperty) && usernameProperty.ValueKind == JsonValueKind.String)
                {
                    string username = usernameProperty.GetString();
                    // Odpowiedni typ danych: string
                    Console.WriteLine("username: " + username);
                    user.Username = username;
                }


                if (responseJsonDocument.RootElement.TryGetProperty("email", out var emailProperty) && emailProperty.ValueKind == JsonValueKind.Number)
                {
                    string email = emailProperty.GetString();
                    // Odpowiedni typ danych: int
                    user.Email = email;
                    Console.WriteLine("email: " + email);

                }

                if (responseJsonDocument.RootElement.TryGetProperty("user_id", out var userIdProperty) && userIdProperty.ValueKind == JsonValueKind.Number)
                {
                    int userId = userIdProperty.GetInt32();
                    // Odpowiedni typ danych: int
                    user.UserId = userIdProperty.GetInt32();
                    Console.WriteLine("UserID: " + userId);

                }

                if (responseJsonDocument.RootElement.TryGetProperty("token", out var tokenProperty) && tokenProperty.ValueKind == JsonValueKind.String)
                {
                    string token = tokenProperty.GetString();
                    // Odpowiedni typ danych: string
                    Console.WriteLine("Token: " + token);
                    user.Token = token;
                }

                IsSignInSuccess = true;
                SignInCompleted?.Invoke(this, true);
            }
            catch(Exception ex)
            {
                ErrorResponse = signInResponse;
                IsSignInSuccess = false;
                SignInCompleted?.Invoke(this, false);
            }

     

            /* if (responseJsonDocument.RootElement.TryGetProperty("response", out var responseProperty) && tokenProperty.ValueKind == JsonValueKind.String)
             {
                 string response = responseProperty.GetString();
                 // Odpowiedni typ danych: string
                 Console.WriteLine("Token: " + response);

             }*/
        }

    }
}
