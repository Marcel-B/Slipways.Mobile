using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Slipways.Mobile.ViewModels
{
    public class ServicePageViewModel : ViewModelBase
    {
        public ServicePageViewModel(
            INavigationService navigationService) : base(navigationService)
        {
            Title = "Werkstätten";
        }

        public override void OnNavigatedFrom(
            INavigationParameters parameters)
        {
        }

        public override void OnNavigatedTo(
            INavigationParameters parameters)
        {
        }
    }
}
