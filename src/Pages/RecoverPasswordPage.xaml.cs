using Microsoft.Maui.Controls;
using System;
using System.IO;

namespace PracticalWork_2.Pages
{
    public partial class RecoverPasswordPage : ContentPage
    {
        public RecoverPasswordPage()
        {
            InitializeComponent();
        }

        private async void OnSendClicked(object sender, EventArgs e)
        {
            string email = EmailEntry.Text?.Trim();

                // Validate email input
            if (string.IsNullOrEmpty(email))
            {
                await DisplayAlert("Error", "Please enter your email.", "OK");
                return;
            }

            string filePath = "users.csv";

            if (!File.Exists(filePath))
            {
                await DisplayAlert("Error", "User database not found.", "OK");
                return;
            }
                // Find the user by email and show their credentials
            string[] lines = File.ReadAllLines(filePath);
            foreach (var line in lines)
            {
                var parts = line.Split(',');

                if (parts.Length >= 4 && parts[2].Equals(email, StringComparison.OrdinalIgnoreCase))
                {
                    string username = parts[1];
                    string password = parts[3];
                    await DisplayAlert("Password Recovery", $"Username: {username}\nPassword: {password}", "OK");
                    return;
                }
            }

            await DisplayAlert("Not Found", "Email not found.", "OK");
        }

        private async void OnBackToLoginClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}
