using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Lands.Views;

namespace Lands
{
    public partial class App : Application
    {
        #region Constructors
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new LoginPage());
        }
        #endregion

        #region Methods
        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
        #endregion
    }
}
