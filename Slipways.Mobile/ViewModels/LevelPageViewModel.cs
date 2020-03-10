using Prism.Events;
using Prism.Navigation;
using Slipways.Mobile.Contracts;
using Slipways.Mobile.Data.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Slipways.Mobile.ViewModels
{
    public class LevelPageViewModel : ViewModelBase
    {
        private IEventAggregator _eventAggregator;
        private IDataStore _dataStore;

        private ObservableCollection<Manufacturer> _manufacturers;
        public ObservableCollection<Manufacturer> Manufacturers
        {
            get => _manufacturers;
            set => SetProperty(ref _manufacturers, value);
        }

        public LevelPageViewModel(
            IEventAggregator eventAggregator,
            IDataStore dataStore,
            INavigationService navigationService) : base(navigationService)
        {
            Title = "Pegel";
            _eventAggregator = eventAggregator;
            _dataStore = dataStore;
            Manufacturers = new ObservableCollection<Manufacturer>();
        }

        public override void OnNavigatedFrom(
            INavigationParameters parameters)
        {
        }

        public override void OnNavigatedTo(
            INavigationParameters parameters)
        {
            foreach (var manufacturer in _dataStore.Manufacturers)
            {
                Manufacturers.Add(manufacturer);
            }
        }
    }
}