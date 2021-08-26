using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Lands.ViewModels
{
    public class LoginViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsRunning { get; set; }
        public bool IsRemembered { get; set; }

        //COMANDS

        public ICommand LoginCommand { get => new RelayCommand(LoginTap); }

        private void LoginTap()
        {
            
        }

        public LoginViewModel()
        {
            IsRemembered = true;

        }

    }
}
