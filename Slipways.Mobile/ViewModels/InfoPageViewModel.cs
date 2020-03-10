using System.Linq;
using Prism.Navigation;
using Slipways.Mobile.Contracts;

namespace Slipways.Mobile.ViewModels
{
    public class InfoPageViewModel : ViewModelBase
    {
        private int _slipwaysCount;
        private IDataStore _dataStore;

        public int SlipwaysCount
        {
            get => _slipwaysCount;
            set => SetProperty(ref _slipwaysCount, value);
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
        }
    }
}
