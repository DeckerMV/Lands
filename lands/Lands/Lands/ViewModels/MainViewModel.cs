using System;
using System.Collections.Generic;
using System.Text;

namespace Lands.ViewModels
{
    public class MainViewModel
    {
        public LoginViewModel Login { get; set; }
        public LandsViewModel Lands { get; set; }

        #region Singleton
        private static MainViewModel instance;
        public static MainViewModel GetInstance()
        {
            if (instance == null)
                return new MainViewModel();
            return instance;
        }
        #endregion

        public MainViewModel()
        {
            instance = this;
            Login = new LoginViewModel();
        }


    }
}
