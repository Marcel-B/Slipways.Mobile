using Prism.Events;
using Prism.Navigation;
using Slipways.Mobile.Contracts;
using Slipways.Mobile.Data;
using Slipways.Mobile.Data.Models;
using Slipways.Mobile.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace Slipways.Mobile.ViewModels
{
    public class MarinaPageViewModel : ListViewModel<Marina>
    {
        private ObservableCollection<Marina> _marinas;
        private IDataStore _dataStore;
        public ICommand ItemTappedCommand { get; set; }

        public ObservableCollection<Marina> Marinas
        {
            get => _marinas;
            set => SetProperty(ref _marinas, value);
        }

        public MarinaPageViewModel(
                        IEventAggregator eventAggregator,
                        IDataStore dataStore,
                        INavigationService navigationService) : base(DataT.Marina, eventAggregator,navigationService)
        {
            Title = "Marinas";
            Marinas = new ObservableCollection<Marina>();
            _dataStore = dataStore;
        }

        public override void OnNavigatedFrom(
            INavigationParameters parameters)
        {
     
        }

        public override void OnNavigatedTo(
            INavigationParameters parameters)
        {
            //if (Marinas.Count == 0)
            //    Update("marina");
        }
    }
}