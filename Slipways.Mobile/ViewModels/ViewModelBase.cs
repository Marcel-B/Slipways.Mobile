using Prism.Mvvm;
using Prism.Navigation;

namespace Slipways.Mobile.ViewModels
{
    public abstract class ViewModelBase : BindableBase, INavigationAware, IDestructible
    {
        private string _title;
        public string Title { get => _title; set => SetProperty(ref _title, value); }

        protected INavigationService NavigationService { get; private set; }

        public ViewModelBase(
            INavigationService navigationService)
        {
            NavigationService = navigationService;
        }

        public void Destroy()
        {
        }

        public abstract void OnNavigatedFrom(
            INavigationParameters parameters);

        public abstract void OnNavigatedTo(
            INavigationParameters parameters);
    }
}
