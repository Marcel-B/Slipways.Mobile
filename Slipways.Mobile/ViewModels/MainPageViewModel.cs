using Prism.Navigation;
using Slipways.Mobile.Helpers;
using Slipways.Mobile.Views;
using System.Windows.Input;
using Xamarin.Forms;

namespace Slipways.Mobile.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public ICommand Navigate { get; set; }

        public MainPageViewModel(
            INavigationService navigationService) : base(navigationService)
        {
            Navigate = new Command(async (sender) =>
            {
                var pageName = sender switch
                {
                    CommandParameter.Slipways => typeof(SlipwaysListPage).Name,
                    CommandParameter.Marinas => typeof(MarinaPage).Name,
                    CommandParameter.Waters => typeof(WaterPage).Name,
                    CommandParameter.Info => typeof(InfoPage).Name,
                    CommandParameter.Services => typeof(ServicePage).Name,
                    CommandParameter.Map => typeof(MapPage).Name,
                    CommandParameter.Levels => typeof(LevelPage).Name,
                    _ => string.Empty
                };
                await navigationService.NavigateAsync(pageName);
            });
            Title = "slipways.de";
        }

        public override void OnNavigatedFrom(
            INavigationParameters parameters)
        {
        }

        public override void OnNavigatedTo(
            INavigationParameters parameters)
        {
        }
    }
}
