using Projekt_WPF_TODO_app.Logic.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Projekt_WPF_TODO_app.Logic
{
    public class ResetPassword
    {
        public bool IsResetSuccess { get; set; }

        public string? ResetEmail { get; set; }

        public event EventHandler<bool>? ResetInCompleted;
        public ICommand? ResetPasswordCommend { get; set; }

        [JsonPropertyName("message")]
        public string? Message { get; set; }
        public ResetPassword()
        {
            ResetPasswordCommend = new RelayCommand(ResetPasswordClick);
        }

        public void ResetPasswordClick()
        {
            var UserData = new
            {
                email = ResetEmail,
            };
            var resetPasswordHandler = new ApiHelper("http://kubpi.pythonanywhere.com/");
            var userDataInJson = JsonSerializer.Serialize(UserData);
            var resetPasswordResponse = resetPasswordHandler.SendPostRequest(userDataInJson, "send-password-reset-token");
            Console.WriteLine("Response: " + resetPasswordResponse);
            Message = resetPasswordResponse;
            try
            {
                var deserializedResponseData = JsonSerializer.Deserialize<Dictionary<string, List<string>>>(resetPasswordResponse) ?? throw new ArgumentException(); ;
                List<string> values = deserializedResponseData.Values.FirstOrDefault() ?? throw new ArgumentException();
                Message = String.Join(" ", values);
                Console.WriteLine("Response: " + Message);
                IsResetSuccess = true;
                ResetInCompleted?.Invoke(this, true);
            }
            catch (Exception)
            {
                Console.WriteLine("Response: " + Message);
                Console.WriteLine("Blad");
                IsResetSuccess = false;
                ResetInCompleted?.Invoke(this, false);
            }
        }
    }
}
