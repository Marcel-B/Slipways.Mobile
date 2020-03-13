using Prism.Events;
using Prism.Navigation;
using Slipways.Mobile.Data.Models;

namespace Slipways.Mobile.ViewModels
{
    public class ServicePageViewModel : ViewModelBase<Service>
    {
        public ServicePageViewModel(
            IEventAggregator eventAggregator,
            INavigationService navigationService) : base("service", eventAggregator, navigationService)
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
