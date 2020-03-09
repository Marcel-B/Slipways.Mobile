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
                    var p = new NavigationParameters();
                    p.Add("slipway", slipway);
                    await NavigationService.NavigateAsync("SlipwayDetails", p);
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
                var slip = await _dataStore.GetSlipwaysAsync();
                foreach (var s in slip)
                {
                    Slipways.Add(s);
                }
            }
        }
    }
}
