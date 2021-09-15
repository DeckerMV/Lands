
using Lands.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Lands.ViewModels
{
    
    public class SingleLandViewModel : BaseViewModel
    {
        private ObservableCollection<Border> borders;
        private ObservableCollection<Currency> currencies;
        private ObservableCollection<Language> languages;

        public Land SelectedLand { get; set; }

        public ObservableCollection<Language> Languages
        {
            get => languages;
            set => SetValue(ref languages, value);
        }

        public ObservableCollection<Border> Borders
        {
            get => borders;
            set => SetValue(ref borders, value);
        }
        public ObservableCollection<Currency> Currencies
        {
            get => currencies;
            set => SetValue(ref currencies, value);
        }

        public SingleLandViewModel(Land selectedLand)
        {
            SelectedLand = selectedLand;
            LoadBorders();
            Currencies = new ObservableCollection<Currency>(SelectedLand.Currencies);
            Languages = new ObservableCollection<Language>(SelectedLand.Languages);
        }

        private void LoadBorders()
        {
            Borders = new ObservableCollection<Border>();
            foreach (string border in SelectedLand.Borders)
            {
                Land land = MainViewModel.GetInstance().Lands
                    .Where(lnd => lnd.Alpha3Code == border)
                    .FirstOrDefault();

                if (land != null)
                {
                    Borders.Add(new Border()
                    {
                        Code = land.Alpha3Code,
                        Name = land.Name
                    });
                }
            }
        }
    }
}
