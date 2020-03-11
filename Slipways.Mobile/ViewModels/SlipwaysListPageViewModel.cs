using Prism.Events;
using Prism.Navigation;
using Slipways.Mobile.Contracts;
using Slipways.Mobile.Data.Models;
using Slipways.Mobile.Events;
using System.Collections.ObjectModel;
using System.Linq;
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
            IEventAggregator eventAggregator,
            IDataStore dataStore) : base(navigationService)
        {
            eventAggregator.GetEvent<UpdateReadyEvent>().Subscribe(Update);
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

        public void Update(
            string payload)
        {
            if (payload == "slipway")
            {
                Slipways.Clear();
                foreach (var slipway in _dataStore.Slipways.OrderBy(_ => _.Name))
                    Slipways.Add(slipway);
            }
        }

        public override void OnNavigatedTo(
            INavigationParameters parameters)
        {
            if (Slipways.Count == 0)
                Update("slipway");
        }
    }
}
