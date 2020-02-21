
using Slipways.Mobile.Infrastructure;
using Slipways.Mobile.Services;
using Slipways.Mobile.ViewModels;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Slipways.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SlipwaysListPage : ContentPage
    {
        public SlipwaysListPage()
        {
            InitializeComponent();
        }

        private void ContentPage_Appearing(
            object sender,
            System.EventArgs e)
        {
            if (BindingContext is SlipwaysListPageViewModel viewModel)
                viewModel.LoadData().SafeFireAndForget(true);
        }
    }
}