using Lands.Models;
using Lands.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace Lands.ViewModels
{
    public class LandsViewModel: BaseViewModel
    {
        private APIServices apiService; 

        private ObservableCollection<Land> lands;
        public ObservableCollection<Land> Lands 
        {
            get => lands;
            set => SetValue(ref lands, value);
        }

        public LandsViewModel()
        {
            apiService = new APIServices();
            LoadLands();
        }

        private async void LoadLands()
        {
            var connection = await apiService.CheckConnection();

            if (!connection.isSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Error", connection.Message, "Accept");
                await Application.Current.MainPage.Navigation.PopAsync();
                return;
            }

            var response = await apiService.GetList<Land>("https://restcountries.eu", "/rest", "/v2/all");
            if (!response.isSuccess)
                await Application.Current.MainPage.DisplayAlert("Error", response.Message, "Accept");

            List<Land> list = (List<Land>) response.Result;
            Lands = new ObservableCollection<Land>(list);
        }
    }
}
