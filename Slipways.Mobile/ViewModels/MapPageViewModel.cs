using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Slipways.Mobile.ViewModels
{
    public class MapPageViewModel : ViewModelBase
    {
        public MapPageViewModel(
            INavigationService navigationService) : base(navigationService)
        {
            Title = "Übersicht";
        }
        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
        }
    }
}