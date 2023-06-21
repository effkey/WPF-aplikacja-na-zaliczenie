using Projekt_WPF_TODO_app.Logic.Helpers;
using System;
using System.IO;
using System.Text.Json;
using System.Windows.Input;

namespace Projekt_WPF_TODO_app.Logic
{
    public class LogIn : User
    {
        public bool IsLogInSuccess { get; set; }
        public string? ErrorResponse { get; set; }
        public ICommand? LogInCommand { get; set; }

        public event EventHandler<bool>? LogInCompleted;

        public LogIn()
        {
            LogInCommand = new RelayCommand(LogInIntoApp);
        }
        private void LogInIntoApp()
        {
            Console.WriteLine("Odpalasie ");
            var logInUserData = new
            {
                username = Username,
                password = Password,
            };
            Console.WriteLine(logInUserData);
            var logInHandler = new ApiHelper("http://kubpi.pythonanywhere.com/");
            var logInDataInJson = JsonSerializer.Serialize(logInUserData);
            var logInResponse = logInHandler.SendPostRequest(logInDataInJson, "login");
            try
            {
                var deserializedResponseData = JsonSerializer.Deserialize<LogIn>(logInResponse) ?? throw new ArgumentException();
                Console.WriteLine("Token logged user: " + deserializedResponseData.Token);
                Console.WriteLine("UserID logged user: " + deserializedResponseData.UserId);
                Console.WriteLine("Response: " + logInResponse);
                SaveLogInSession(deserializedResponseData);
                IsLogInSuccess = true;
                LogInCompleted?.Invoke(this, true);
            }
            catch (Exception)
            {
                Console.WriteLine("Response: " + logInResponse);
                Console.WriteLine("Blad");
                IsLogInSuccess = false;
                ErrorResponse = logInResponse;
                LogInCompleted?.Invoke(this, false);
            }
        }

        public void SaveLogInSession(User deserializedResponseData)
        {
            var session = new
            {
                username = Username,
                token = deserializedResponseData.Token,
                userid = deserializedResponseData.UserId,
            };
            Console.WriteLine(session);
            string sessionInJsonFormat = JsonSerializer.Serialize(session);
            File.WriteAllText("session.json", sessionInJsonFormat);
        }

        public bool ReadLogInSession()
        {
            if (File.Exists("session.json"))
            {

                string sessionJson = File.ReadAllText("session.json");
                Console.WriteLine("session " + sessionJson);
                var session = JsonSerializer.Deserialize<LogIn>(sessionJson);
                Console.WriteLine("session " + session?.Username);
                Console.WriteLine("session " + session?.Token);
                return true;
            }
            else { return false; }
        }
    }
}
