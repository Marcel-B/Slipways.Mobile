using System;
using System.Linq;
using Prism.Navigation;
using Slipways.Mobile.Contracts;
using Slipways.Mobile.Data.Models;

namespace Slipways.Mobile.ViewModels
{
    public class WaterPageViewModel : ViewModelBase
    {
        private ISlipwaysDatabase _db;
        private string _username;
        public string Username
        {
            get => _username;
            set => SetProperty(ref _username, value);
        }

        public WaterPageViewModel(
            ISlipwaysDatabase db,
            INavigationService navigationService) : base(navigationService)
        {
            _db = db;
        }

        public override void OnNavigatedFrom(
            INavigationParameters parameters)
        {
        }

        public async override void OnNavigatedTo(
            INavigationParameters parameters)
        {
            var users = await _db.GetRecordsAsync<User>();
            Username = users.First().Name;
        }
    }
}
