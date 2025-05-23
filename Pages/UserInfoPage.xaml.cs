using Microsoft.Maui.Controls;
using System;
using System.IO;
using System.Linq;

namespace PracticalWork_2.Pages
{
    public partial class UserInfoPage : ContentPage
    {
        public string Username { get; set; }

        public UserInfoPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing(); // Called when the page becomes visible
            
        }

        
        private async void OnInfoClicked(object sender, EventArgs e)
        {
            string username = UsernameEntry.Text?.Trim();

                // Validate username
            if (string.IsNullOrEmpty(username))
            {
                await DisplayAlert("Error", "Please enter your username.", "OK");
                return;
            }

            string filePath = "users.csv";

            if (!File.Exists(filePath))
            {
                await DisplayAlert("Error", "User database not found.", "OK");
                return;
            }

                // Search for user info by username
            string[] lines = File.ReadAllLines(filePath);
            foreach (var line in lines)
            {
                var parts = line.Split(',');

                if (parts.Length >= 4 && parts[1].Equals(username, StringComparison.OrdinalIgnoreCase))
                {
                    string name = parts[0];
                    string username1 = parts[1];
                    string password = parts[3];
                    string operations = parts[4];

                    await DisplayAlert("User info", $"Name: {name}\nUsername: {username1}\nPassword: {password}\nNº Operations: {operations} ", "OK"); //añadir numero de operaciones
                    return;
                }
            }

            await DisplayAlert("Not Found", "Username not found.", "OK");
        }

        private async void OnBackClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("..");
        }
        private async void OnLogoutClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }
        private void OnExitClicked(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
