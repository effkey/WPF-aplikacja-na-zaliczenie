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
                var deserializedResponseData = JsonSerializer.Deserialize<User>(signInResponse) ?? throw new ArgumentException();
                Console.WriteLine("Response: " + Response);
                //SaveLogInSession(deserializedResponseData);
                IsSignInSuccess = true;
                SignInCompleted?.Invoke(this, true);
                
            }
            catch (Exception)
            {
                Console.WriteLine("Response: " + Response);
                Console.WriteLine("Blad");
                IsSignInSuccess = false;
                SignInCompleted?.Invoke(this, false);
            }
        }

    }
}
