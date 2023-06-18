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
    public class SignIn : User
    {
        public bool IsSignInSuccess { get; set; }
        public string? Response { get; set; }

        public event EventHandler<bool>? SignInCompleted;
        public ICommand? SignInCommend { get; set; }

        public SignIn()
        {
            SignInCommend = new RelayCommand(SignInIntoApp);
        }
        public void SignInIntoApp()
        {
            Console.WriteLine("dada");
            var signInUserData = new
            {
                username = Username,
                email = Email,
                password = Password,
                password2 = Password2,
            };
            var signInHandler = new ApiHelper("http://kubpi.pythonanywhere.com/");
            var signInDataInJson = JsonSerializer.Serialize(signInUserData);
            var signInResponse = signInHandler.SendPostRequest(signInDataInJson, "register");
            Response = signInResponse;
            try
            {

                var deserializedResponseData = JsonSerializer.Deserialize<SignIn>(signInResponse);
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
                //ErrorResponse = signInResponse;
                SignInCompleted?.Invoke(this, false);
            }
        }

    }
}
