using Lands.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lands.ViewModels
{
    public class MainViewModel
    {
        public LoginViewModel LoginVM { get; set; }
        public LandsViewModel LandsVM { get; set; }
        public SingleLandViewModel SingleLandVM { get; set; }
        public List<Land> Lands { get; set; }

        #region Singleton
        private static MainViewModel instance;
        public static MainViewModel GetInstance()
        {
            if (instance == null)
                return new MainViewModel();
            return instance;
        }
        #endregion

        private MainViewModel()
        {
            instance = this;
            LoginVM = new LoginViewModel();
        }


    }
}
