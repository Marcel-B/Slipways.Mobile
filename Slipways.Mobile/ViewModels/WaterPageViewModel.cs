using System.Collections.ObjectModel;
using System.Linq;
using Prism.Events;
using Prism.Navigation;
using Slipways.Mobile.Contracts;
using Slipways.Mobile.Data.Models;
using Slipways.Mobile.Events;

namespace Slipways.Mobile.ViewModels
{
    public class WaterPageViewModel : ViewModelBase<Water>
    {
        private readonly IRepositoryWrapper _dataStore;

        private string _username;
        public string Username
        {
            get => _username;
            set => SetProperty(ref _username, value);
        }

        public WaterPageViewModel(
            IRepositoryWrapper rep,
            IEventAggregator eventAggregator,
            INavigationService navigationService) : base("water", eventAggregator,  navigationService)
        {
            Title = "Gewässer";
            _dataStore = rep;
        }

        public override void OnNavigatedFrom(
            INavigationParameters parameters)
        {
        }

        public override async void OnNavigatedTo(
            INavigationParameters parameters)
        {
            var waters = await _dataStore.Waters.GetAllAsync();
            Data.Clear();
            foreach (var water in waters)
            {
                Data.Add(water);
            }
            //if (Waters.Count == 0)
            //    Update("water");
        }
    }
}
