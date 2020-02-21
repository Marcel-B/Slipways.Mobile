using Slipways.Mobile.Data.Models;
using Slipways.Mobile.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Slipways.Mobile.ViewModels
{
    public class SlipwaysListPageViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Slipway> _slipways;
        private GraphQlService _service;

        public ObservableCollection<Slipway> Slipways
        {
            get => _slipways;
            set
            {
                _slipways = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Slipways"));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        public SlipwaysListPageViewModel()
        {
            _service = new GraphQlService();
            Slipways = new ObservableCollection<Slipway>();
        }

        public SlipwaysListPageViewModel(
            GraphQlService service = null)
        {
            _service = service ?? new GraphQlService();
            Slipways = new ObservableCollection<Slipway>();
        }

        public async Task LoadData()
        {
            var data = await _service.GetSlipwaysASync();
            var db = App.Database;
            Slipways.Clear();
            foreach (var slipway in data)
            {
                Slipways.Add(slipway);

                var water = await db.GetByUuidAsync<Water>(slipway.Water.Pk);
                int a;
                if (water == null)
                    a = await db.SaveRecordAsync(slipway.Water);

                a = await db.SaveRecordAsync(slipway.Water);
                slipway.Water.Id = a;
                int b = 0;

                var slip = await db.GetByUuidAsync<Slipway>(slipway.Pk);

                if (slip == null)
                {
                    b = await db.SaveRecordAsync(slipway);
                }
                else
                {
                    b = slip.Id;
                }




                System.Console.WriteLine($"Index is {i}");
            }
        }
    }
}
