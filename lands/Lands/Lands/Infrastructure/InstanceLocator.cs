using System;
using System.Collections.Generic;
using System.Text;
using Lands.ViewModels;

namespace Lands.Infrastructure
{
    class InstanceLocator
    {
        public MainViewModel Main { get; set; }

        public InstanceLocator()
        {
            Main = new MainViewModel();
        }

    }
}
