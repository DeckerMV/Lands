
using Lands.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Lands.ViewModels
{
    
    public class SingleLandViewModel : BaseViewModel
    {
        private ObservableCollection<Border> borders;

        public Land SelectedLand { get; set; }

        public ObservableCollection<Border> Borders 
        {
            get => borders;
            set => SetValue(ref borders, value);
        }

        public SingleLandViewModel(Land selectedLand)
        {
            SelectedLand = selectedLand;
            LoadBorders();
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
