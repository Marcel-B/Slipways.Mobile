using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Slipways.Mobile.ViewModels
{
    public class MarinaPageViewModel : ViewModelBase
    {
        public MarinaPageViewModel(
            INavigationService navigationService) : base(navigationService)
        {
            Title = "Marinas";
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