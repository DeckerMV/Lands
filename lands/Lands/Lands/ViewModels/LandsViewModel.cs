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
    public class LandsViewModel : BaseViewModel
    {
        #region Services
        private APIServices apiService;
        #endregion

        #region Fields
        private ObservableCollection<LandItemViewModel> lands;
        private bool isRefreshing;
        private string filter;
        private List<Land> ogList;
        #endregion

        #region Properties
        public ObservableCollection<LandItemViewModel> Lands
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
        {//to re-load the Lands list
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
                Lands = new ObservableCollection<LandItemViewModel>(ToLandItemViewModel());
            }
            else
            {
                Lands = new ObservableCollection<LandItemViewModel>(
                    ToLandItemViewModel().Where(
                        lnd => lnd.Name.ToLower().Contains(Filter.ToLower()) ||
                            lnd.Capital.ToLower().Contains(Filter.ToLower())));
            }
        }

        private async void LoadLands()
        {
            IsRefreshing = true;
            Response connection = await apiService.CheckConnection();

            if (!connection.IsSuccess)
            {
                IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert("Error", connection.Message, "Accept");
                await Application.Current.MainPage.Navigation.PopAsync();
                return;
            }

            Response landsListResponse = await apiService.GetList<Land>("https://restcountries.eu", "/rest", "/v2/all");
            if (!landsListResponse.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Error", landsListResponse.Message, "Accept");
                await Application.Current.MainPage.Navigation.PopAsync();
                return;
            }

            ogList = landsListResponse.Result as List<Land>;
            Lands = new ObservableCollection<LandItemViewModel>(ToLandItemViewModel());
            IsRefreshing = false;
        }

        private IEnumerable<LandItemViewModel> ToLandItemViewModel()
        {
            return ogList.Select(og => new LandItemViewModel()
            {
                Alpha2Code = og.Alpha2Code,
                AltSpellings = og.AltSpellings,
                Alpha3Code = og.Alpha3Code,
                Area = og.Area,
                Borders = og.Borders,
                CallingCodes = og.CallingCodes,
                Capital = og.Capital,
                Cioc = og.Cioc,
                Currencies = og.Currencies,
                Demonym = og.Demonym,
                Flag = og.Flag,
                Gini = og.Gini,
                Languages = og.Languages,
                Latlng = og.Latlng,
                Name = og.Name,
                NativeName = og.NativeName,
                NumericCode = og.NumericCode,
                Population = og.Population,
                Region = og.Region,
                RegionalBlocs = og.RegionalBlocs,
                Subregion = og.Subregion,
                Timezones = og.Timezones,
                TopLevelDomain = og.TopLevelDomain,
                Translations = og.Translations
            });
        }
    }
}
