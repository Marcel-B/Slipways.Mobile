using Prism.Events;
using Prism.Navigation;
using Slipways.Mobile.Contracts;
using Slipways.Mobile.Data.Models;
using Slipways.Mobile.Helpers;

namespace Slipways.Mobile.ViewModels
{
    public class WaterPageViewModel : ListViewModel<Water>
    {
        private readonly IRepositoryWrapper _repository;
        public WaterPageViewModel(
            IRepositoryWrapper repository,
            IEventAggregator eventAggregator,
            INavigationService navigationService) : base(DataT.Water, eventAggregator, navigationService)
        {
            Title = "Gewässer";
            _repository = repository;
        }

        public override void OnNavigatedFrom(
            INavigationParameters parameters)
        { }

        public override async void OnNavigatedTo(
            INavigationParameters parameters)
        {
            var waters = await _repository.Waters.GetAllAsync();
            //Data.Clear();
            //foreach (var water in waters)
            //    Data.Add(water);
        }
    }
}
