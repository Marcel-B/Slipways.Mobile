using Prism.Events;
using Prism.Navigation;
using Prism.Services;
using Slipways.Mobile.Contracts;
using Slipways.Mobile.Events;
using Slipways.Mobile.Helpers;
using Slipways.Mobile.Views;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Slipways.Mobile.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private bool _running;
        private IPageDialogService _dialogService;
        private IDataStore _dataStore;

        public bool Running
        {
            get => _running;
            set => SetProperty(ref _running, value);
        }

        public ICommand Navigate { get; set; }

        public MainPageViewModel(
            IPageDialogService dialogService,
            IDataStore dataStore,
            IEventAggregator eventAggregator,
            INavigationService navigationService) : base(navigationService)
        {
            _dialogService = dialogService;
            _dataStore = dataStore;
            eventAggregator.GetEvent<InitializationReadyEvent>().Subscribe(Ready);

            Navigate = new Command(async (sender) =>
            {
                var pageName = sender switch
                {
                    CommandParameter.Slipways => typeof(SlipwaysListPage).Name,
                    CommandParameter.Marinas => typeof(MarinaPage).Name,
                    CommandParameter.Waters => typeof(WaterPage).Name,
                    CommandParameter.Info => typeof(InfoPage).Name,
                    CommandParameter.Services => typeof(ServicePage).Name,
                    CommandParameter.Map => typeof(MapPage).Name,
                    CommandParameter.Levels => typeof(LevelPage).Name,
                    _ => string.Empty
                };
                await _dialogService.DisplayAlertAsync("Alert", "You have been alerted", "OK");

                //await navigationService
                //.NavigateAsync(pageName)
                //.ConfigureAwait(false);
            });
            Title = "slipways.de";
            Running = true;
        }

        public async void Ready(bool rdy)
        {
            //await _dialogService.DisplayAlertAsync("Ready", "Initialization of data ready", "OK");
        }

        public override void OnNavigatedFrom(
            INavigationParameters parameters)
        {
        }

        public override void OnNavigatedTo(
            INavigationParameters parameters)
        {
            //await _dataStore.LoadData()
            //    .ConfigureAwait(false);
        }
    }
}
