using Prism.Navigation;
using Slipways.Mobile.Contracts;
using Slipways.Mobile.Data.Models;
using Xamarin.Forms.Maps;

namespace Slipways.Mobile.ViewModels
{
    public class MapPageViewModel : ViewModelBase
    {
        private Map _map;
        private IDataStore _dataStore;

        public Map Map
        {
            get => _map;
            set => SetProperty(ref _map, value);
        }

        public MapPageViewModel(
            IDataStore dataStore,
            INavigationService navigationService) : base(navigationService)
        {
            _dataStore = dataStore;
            Title = "Übersicht";
        }
        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            var slipways = _dataStore.Slipways;
            var position = new Position(51.312801, 9.481544);
            var mapSpan = new MapSpan(position, 10, 10);
            Map = new Map(mapSpan);
            foreach (var slipway in slipways)
            {
                var pos = new Position(slipway.Latitude, slipway.Longitude);
                var pin = new Pin
                {
                    Label = slipway.Name,
                    Position = pos,
                    Type = PinType.Place,
                    Address = slipway.Street
                };
                Map.Pins.Add(pin);
            }
            Map.MapType = MapType.Satellite;
        }
    }
}