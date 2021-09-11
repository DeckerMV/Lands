
using Lands.Models;

namespace Lands.ViewModels
{
    
    public class SingleLandViewModel
    {
        public Land SelectedLand
        {
            get;
            set;
        }

        public SingleLandViewModel(Land selectedLand)
        {
            SelectedLand = selectedLand;
        }
    }
}
