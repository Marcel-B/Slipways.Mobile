using Prism.Events;
using Prism.Navigation;
using Slipways.Mobile.Contracts;
using Slipways.Mobile.Data.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Slipways.Mobile.Helpers;

namespace Slipways.Mobile.ViewModels
{
    public class LevelPageViewModel : ListViewModel<Station>
    {
        public LevelPageViewModel(
            IEventAggregator eventAggregator,
            IDataStore dataStore,
            INavigationService navigationService) : base(DataT.Station, eventAggregator, navigationService)
        {
            Title = "Pegel";
        }

        public override void OnNavigatedFrom(
            INavigationParameters parameters)
        {
        }

        public override void OnNavigatedTo(
            INavigationParameters parameters)
        {
            //foreach (var manufacturer in _dataStore.Manufacturers)
            //{
            //    Manufacturers.Add(manufacturer);
            //}
        }
    }
}