using Prism.Navigation;

namespace Slipways.Mobile.ViewModels
{
    public class InfoPageViewModel : ViewModelBase
    {
        public InfoPageViewModel(
            INavigationService navigationService)
            :base(navigationService)
        {
            Title = "Info";
        }

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
        }
    }
}
