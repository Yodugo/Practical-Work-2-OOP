using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace PracticalWork_2.Pages
{
    public partial class LoginPage : ContentPage
    {
        private string userFilePath;

        public LoginPage()
        {
            InitializeComponent();

            userFilePath = "users.csv"; // Path to the CSV file storing user data
        }

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            string username = UsernameEntry.Text?.Trim();
            string password = PasswordEntry.Text;

                // Validation for empty fields
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                await DisplayAlert("Error", "Please enter both username and password.", "OK");
                return;
            }
                 // Check if the user data file exists
            if (!File.Exists(userFilePath))
            {
                await DisplayAlert("Error", "No users found. Please register first.", "OK");
                return;
            }
                // Read and validate credentials
            var lines = File.ReadAllLines(userFilePath);
            foreach (var line in lines)
            {
                var parts = line.Split(',');
                // Check if the username and password match
                if (parts.Length >= 4 && parts[1] == username && parts[3] == password)
                {
                    await DisplayAlert("Success", $"Welcome, {parts[0]}!", "OK");
                    await Shell.Current.GoToAsync("//MainPage");
                    return;
                }
            }

            await DisplayAlert("Error", "Invalid username or password.", "OK");
        }

        private async void OnRegisterClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//RegisterPage");
        }

        private void OnExitClicked(object sender, EventArgs e)
        {
            Environment.Exit(0);

        }
        private async void OnForgotPasswordClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("RecoverPasswordPage");
        }

        
    }
}
