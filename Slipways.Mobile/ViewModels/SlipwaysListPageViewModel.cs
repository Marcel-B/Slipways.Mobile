using Prism.Navigation;
using Slipways.Mobile.Contracts;
using Slipways.Mobile.Data.Models;
using Slipways.Mobile.Helpers;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Slipways.Mobile.ViewModels
{
    public class SlipwaysListPageViewModel : ViewModelBase
    {
        private ObservableCollection<Slipway> _slipways;
        private IDataStore _dataStore;
        public ICommand ItemTappedCommand { get; set; }

        public ObservableCollection<Slipway> Slipways
        {
            get => _slipways;
            set => SetProperty(ref _slipways, value);
        }

        public SlipwaysListPageViewModel(
            INavigationService navigationService,
            IDataStore dataStore) : base(navigationService)
        {
            Slipways = new ObservableCollection<Slipway>();
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

        public async override void OnNavigatedTo(
            INavigationParameters parameters)
        {
            if (Slipways.Count == 0)
            {
                System.Console.WriteLine("Slipways contains no element");
                var slipways = await _dataStore.GetSlipwaysAsync();
                foreach (var slipway in slipways)
                {
                    Slipways.Add(slipway);
                }
            }
        }
    }
}
