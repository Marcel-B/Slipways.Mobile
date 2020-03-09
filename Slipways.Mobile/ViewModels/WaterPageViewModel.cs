using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Prism.Navigation;
using Slipways.Mobile.Contracts;
using Slipways.Mobile.Data;
using Slipways.Mobile.Data.Models;
using Slipways.Mobile.Helpers;

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
            INavigationService navigationService) : base(navigationService)
        {
            Waters = new ObservableCollection<Water>();
            Title = "Gewässer";
            _dataStore = dataStore;
        }

        public override void OnNavigatedFrom(
            INavigationParameters parameters)
        {
        }

        public async override void OnNavigatedTo(
            INavigationParameters parameters)
        {
            if (Waters.Count == 0)
            {
                System.Console.WriteLine("Waters contains no element");
                var waters = await _dataStore.GetWatersAsync();
                foreach (var water in waters)
                {
                    Waters.Add(water);
                }
                //foreach (var s in slip)
                //{
                //    Waters.Add(s);
                //}
            }
            //var users = _db.GetAll<User>();
            //Username = users.First().Name;
        }
    }
}
