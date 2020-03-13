using Prism.Events;
using Prism.Navigation;
using Slipways.Mobile.Contracts;
using Slipways.Mobile.Data.Models;
using Slipways.Mobile.Helpers;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace Slipways.Mobile.ViewModels
{
    public class SlipwaysListPageViewModel : ListViewModel<Slipway>
    {
        private IDataStore _dataStore;
        public ICommand ItemTappedCommand { get; set; }

        public SlipwaysListPageViewModel(
            INavigationService navigationService,
            IEventAggregator eventAggregator,
            IDataStore dataStore) : base(
                DataT.Slipway, 
                eventAggregator, 
                navigationService)
        {
            ItemTappedCommand = new Command(async (sender) =>
            {
                if (sender is Slipway slipway)
                {
                    var navigationParameters = new NavigationParameters
                    {
                        { "slipway", slipway }
                    };
                    await NavigationService.NavigateAsync("SlipwayDetails", navigationParameters);
                }
            });
            _dataStore = dataStore;
        }

        public override void OnNavigatedFrom(
            INavigationParameters parameters)
        {
        }

        public override void OnNavigatedTo(
            INavigationParameters parameters)
        {
            //if (Slipways.Count == 0)
            //    Update("slipway");
        }
    }
}
