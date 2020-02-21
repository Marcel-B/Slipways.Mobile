using Slipways.Mobile.Views;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace Slipways.Mobile.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        public ICommand SlipwaysCommand { get; set; }
        private string _title;
        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Title"));
            }
        }

        public MainPageViewModel()
        {
            SlipwaysCommand = new Command(async (sender) =>
            {
                if (sender is MainPage view)
                {
                    await view.Navigation.PushAsync(new SlipwaysListPage());
                }
            });

            Title = "slipways.de";
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
