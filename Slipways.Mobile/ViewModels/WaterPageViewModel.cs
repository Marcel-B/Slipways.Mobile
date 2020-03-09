using System;
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

        private IGraphQLService _graphQLService;
        private IRepository _db;
        private string _username;
        public string Username
        {
            get => _username;
            set => SetProperty(ref _username, value);
        }

        public WaterPageViewModel(
            IRepository db,
            IGraphQLService graphQLService,
            INavigationService navigationService) : base(navigationService)
        {
            Title = "Gewässer";
            _graphQLService = graphQLService;
            _db = db;
        }

        public override void OnNavigatedFrom(
            INavigationParameters parameters)
        {
        }

        public async override void OnNavigatedTo(
            INavigationParameters parameters)
        {
            if (Waters == null)
                Waters = new ObservableCollection<Water>();

            if (Waters.Count > 0)
                return;

            var result = await _graphQLService.FetchValuesAsync<WatersResponse>(Queries.Waters);
            foreach (var water in result.Waters)
            {
                var w = new Water
                {
                    Longname = water.Longname,
                    Shortname = water.Shortname
                };
                Waters.Add(w);
            }
            var users = _db.GetAll<User>();
            Username = users.First().Name;
        }
    }
}
