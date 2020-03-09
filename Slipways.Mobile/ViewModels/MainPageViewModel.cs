using Prism.Navigation;
using System.Windows.Input;
using Xamarin.Forms;

namespace Slipways.Mobile.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public ICommand SlipwaysCommand { get; set; }
        public ICommand InfoPage { get; set; }
        public ICommand ToWaterPage { get; set; }

        public MainPageViewModel(
            INavigationService navigationService) : base(navigationService)
        {
            SlipwaysCommand = new Command(async (sender) =>
            {
                await navigationService.NavigateAsync("SlipwaysListPage");
            });

            InfoPage = new Command(async (sender) =>
            {
                await navigationService.NavigateAsync("InfoPage");
            });

            ToWaterPage = new Command(async (sender) =>
            {
                await navigationService.NavigateAsync("WaterPage");
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
