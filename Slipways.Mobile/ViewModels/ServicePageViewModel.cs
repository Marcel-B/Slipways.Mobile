using Prism.Events;
using Prism.Navigation;
using Slipways.Mobile.Data.Models;
using Slipways.Mobile.Helpers;

namespace Slipways.Mobile.ViewModels
{
    public class ServicePageViewModel : ListViewModel<Service>
    {
        public ServicePageViewModel(
            IEventAggregator eventAggregator,
            INavigationService navigationService) : base(DataT.Service, eventAggregator, navigationService)
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
