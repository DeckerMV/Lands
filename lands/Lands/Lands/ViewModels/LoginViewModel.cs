using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Lands.Views;

namespace Lands.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {

        #region Fields
        private string email;
        private string password;
        private bool isRunning;
        private bool isEnabled;
        #endregion

        #region Properties
        public string Email
        {
            get => email;
            set => SetValue(ref email, value);
        }
        public string Password
        {
            get => password;
            set => SetValue(ref password, value);
        }
        public bool IsRunning
        {
            get => isRunning;
            set => SetValue(ref isRunning, value);
        }
        public bool IsRemembered { get; set; }
        public bool IsEnabled
        {
            get => isEnabled;
            set => SetValue(ref isEnabled, value);
        }
        #endregion

        #region Commands
        public ICommand LoginCommand { get => new RelayCommand(LoginTap); } 
        #endregion

        public LoginViewModel()
        {
            IsRemembered = true;
            IsEnabled = true;
        }

        private async void LoginTap()
        {
            if (string.IsNullOrEmpty(Email))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "You must enter an email.", "Accept");
                return;
            }

            if (string.IsNullOrEmpty(Password))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "You must enter a password.", "Accept");
                return;
            }

            //Email nor password are empty
            IsRunning = true;
            IsEnabled = false;

            if (Email != "erickmx@outlook.com" || Password != "erick")
            {
                //failed
                IsRunning = false;
                IsEnabled = true;
                await Application.Current.MainPage.
                    DisplayAlert("Error", "Email or password incorrect", "Accept");
                Password = string.Empty;
                return;
            }

            IsRunning = false;
            IsEnabled = true;

            Email = string.Empty;
            Password = string.Empty;

            MainViewModel.GetInstance().LandsVM = new LandsViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new LandsPage());

        }

    }
}
