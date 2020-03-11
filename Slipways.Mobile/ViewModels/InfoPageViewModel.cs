using System.Linq;
using Prism.Navigation;
using Slipways.Mobile.Contracts;

namespace Slipways.Mobile.ViewModels
{
    public class InfoPageViewModel : ViewModelBase
    {
        private int _slipwaysCount;
        private int _watersCount;
        private int _marinasCount;

        private IDataStore _dataStore;

        public int SlipwaysCount
        {
            get => _slipwaysCount;
            set => SetProperty(ref _slipwaysCount, value);
        }

        public int WatersCount
        {
            get => _watersCount;
            set => SetProperty(ref _watersCount, value);
        }

        public int MarinasCount
        {
            get => _marinasCount;
            set => SetProperty(ref _marinasCount, value);
        }

        public InfoPageViewModel(
            IDataStore dataStore,
            INavigationService navigationService)
            :base(navigationService)
        {
            Title = "Info";
            _dataStore = dataStore;
        }

        public override void OnNavigatedFrom(
            INavigationParameters parameters)
        {
        }

        public override void OnNavigatedTo(
            INavigationParameters parameters)
        {
            SlipwaysCount = _dataStore.Slipways.Count();
            WatersCount = _dataStore.Waters.Count();
            MarinasCount = _dataStore.Marinas.Count();
        }
    }
}
