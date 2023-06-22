using Projekt_WPF_TODO_app.Logic.Helpers;
using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows.Input;
using Projekt_WPF_TODO_app.Logic;

namespace Projekt_WPF_TODO_app.Logic
{
    public class LogIn 
    {

        public User User { get; set; }
        public bool IsLogInSuccess { get; set; }
        public string? ErrorResponse { get; set; }
        public ICommand? LogInCommand { get; set; }
        public string NewUserPassword{ get; set; }
        public string NewUserName { get; set; }
        public string NewUserToken { get; set; }

        public event EventHandler<bool>? LogInCompleted;

        public User user { get; set; }

        MainWindow mainWindow;
        public LogIn(User user, MainWindow mainWindow)
        {
            this.user = user;
            LogInCommand = new RelayCommand(LogInIntoApp);
        }
        private void LogInIntoApp()
        {
            user.Username = NewUserName;
            user.Password = NewUserPassword;

           // Console.WriteLine("Odpalasie ");
            var logInUserData = new
            {
                username = user.Username,
                password = user.Password,
            };
           
            var logInHandler = new ApiHelper("http://kubpi.pythonanywhere.com/");
            var logInDataInJson = JsonSerializer.Serialize(logInUserData);
            var logInResponse = logInHandler.SendPostRequest(logInDataInJson, "login");
           // Console.WriteLine(logInResponse);
            var responseJsonDocument = JsonDocument.Parse(logInResponse);

            if (responseJsonDocument.RootElement.TryGetProperty("token", out var tokenProperty) && tokenProperty.ValueKind == JsonValueKind.String)
            {
                string token = tokenProperty.GetString();
                // Odpowiedni typ danych: string
                Console.WriteLine("Token: " + token);
                user.Token = token;
            }

            if (responseJsonDocument.RootElement.TryGetProperty("user_id", out var userIdProperty) && userIdProperty.ValueKind == JsonValueKind.Number)
            {
                int userId = userIdProperty.GetInt32();
                // Odpowiedni typ danych: int
                user.UserId = userIdProperty.GetInt32();
                Console.WriteLine("UserID: " + userId);

            }
            IsLogInSuccess = true;
            LogInCompleted?.Invoke(this, true);
      
        }

        public void SaveLogInSession(User deserializedResponseData)
        {
            var session = new
            {
                username = user.Username,
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
                var session = JsonSerializer.Deserialize<User>(sessionJson);
     
                return true;
            }
            else { return false; }
        }
    }
}
