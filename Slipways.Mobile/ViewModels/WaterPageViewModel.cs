using System.Collections.ObjectModel;
using System.Linq;
using Prism.Events;
using Prism.Navigation;
using Slipways.Mobile.Contracts;
using Slipways.Mobile.Data.Models;
using Slipways.Mobile.Events;

namespace Slipways.Mobile.ViewModels
{
    public class WaterPageViewModel : ViewModelBase
    {
        private ObservableCollection<Water> _waters;

        public ObservableCollection<Water> Waters
        {
            get => _waters;
            set => SetProperty(ref _waters, value);
        }

        private readonly IDataStore _dataStore;

        private string _username;
        public string Username
        {
            get => _username;
            set => SetProperty(ref _username, value);
        }

        public WaterPageViewModel(
            IDataStore dataStore,
            IEventAggregator eventAggregator,
            INavigationService navigationService) : base(navigationService)
        {
            Waters = new ObservableCollection<Water>();
            Title = "Gewässer";
            _dataStore = dataStore;
            eventAggregator.GetEvent<UpdateReadyEvent>().Subscribe(Update, ThreadOption.UIThread);
        }

        public void Update(
            string value)
        {
            if (value == "water")
            {
                Waters.Clear();
                foreach (var water in _dataStore.Waters.OrderBy(_ => _.Longname))
                    Waters.Add(water);
            }
        }

        public override void OnNavigatedFrom(
            INavigationParameters parameters)
        {
        }

        public override void OnNavigatedTo(
            INavigationParameters parameters)
        {
            if (Waters.Count == 0)
                Update("water");
        }
    }
}
