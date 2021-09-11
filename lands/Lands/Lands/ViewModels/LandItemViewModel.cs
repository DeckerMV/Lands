using GalaSoft.MvvmLight.Command;
using Lands.Models;
using Lands.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Lands.ViewModels
{
    public class LandItemViewModel : Land
    {
        public ICommand SelectLandCommand
        {
            get => new RelayCommand(SelectLand);
        }

        private async void SelectLand()
        {
            MainViewModel.GetInstance().SingleLandVM = new SingleLandViewModel(this);
            await Application.Current.MainPage.Navigation.PushAsync(new SingleLandPage());
        }
    }
}
