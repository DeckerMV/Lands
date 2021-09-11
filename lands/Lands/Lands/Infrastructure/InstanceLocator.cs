using System;
using System.Collections.Generic;
using System.Text;
using Lands.ViewModels;

namespace Lands.Infrastructure
{
    class InstanceLocator
    {
        public MainViewModel MainVM { get; set; }

        public InstanceLocator()
        {
            MainVM = MainViewModel.GetInstance();
        }

    }
}
