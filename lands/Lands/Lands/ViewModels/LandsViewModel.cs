using GalaSoft.MvvmLight.Command;
using Lands.Models;
using Lands.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Lands.ViewModels
{
    public class LandsViewModel: BaseViewModel
    {
        #region Services
        private APIServices apiService;
        #endregion

        #region Fields
        private ObservableCollection<Land> lands;
        private bool isRefreshing;
        private string filter;
        private List<Land> ogList;
        #endregion

        #region Properties
        public ObservableCollection<Land> Lands
        {
            get => lands;
            set => SetValue(ref lands, value);
        }
        public bool IsRefreshing 
        { 
            get => isRefreshing;
            set => SetValue(ref isRefreshing, value);
        }
        public string Filter
        {
            get => filter;
            set
            {
                SetValue(ref filter, value);
                Search();
            }
        }
        #endregion

        #region Commands
        public ICommand RefreshCommand 
        {
            get => new RelayCommand(LoadLands);
        }
        public ICommand SearchCommand
        {
            get => new RelayCommand(Search);
        }

        #endregion

        public LandsViewModel()
        {
            apiService = new APIServices();
            LoadLands();
        }

        private void Search()
        {
            if (string.IsNullOrEmpty(Filter))
            {
                Lands = new ObservableCollection<Land>(ogList);
            }
            else
            {
                Lands = new ObservableCollection<Land>(
                    ogList.Where(
                        lnd => lnd.Name.ToLower().Contains(Filter.ToLower()) ||
                            lnd.Capital.ToLower().Contains(Filter.ToLower())));
            }
        }

        private async void LoadLands()
        {
            IsRefreshing = true;
            var connection = await apiService.CheckConnection();

            if (!connection.isSuccess)
            {
                IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert("Error", connection.Message, "Accept");
                await Application.Current.MainPage.Navigation.PopAsync();
                return;
            }

            var response = await apiService.GetList<Land>("https://restcountries.eu", "/rest", "/v2/all");
            if (!response.isSuccess)
                await Application.Current.MainPage.DisplayAlert("Error", response.Message, "Accept");

            ogList = (List<Land>) response.Result;
            Lands = new ObservableCollection<Land>(ogList);
            IsRefreshing = false;
        }
    }
}
