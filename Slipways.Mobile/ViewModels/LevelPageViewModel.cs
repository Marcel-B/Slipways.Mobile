using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Slipways.Mobile.ViewModels
{
    public class LevelPageViewModel : ViewModelBase
    {
        public LevelPageViewModel(
            INavigationService navigationService) : base(navigationService)
        {
            Title = "Pegel";
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