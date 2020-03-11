using Prism.Events;
using Prism.Navigation;
using Slipways.Mobile.Contracts;
using Slipways.Mobile.Data;
using Slipways.Mobile.Data.Models;
using Slipways.Mobile.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace Slipways.Mobile.ViewModels
{
    public class MarinaPageViewModel : ViewModelBase
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
INavigationService navigationService) : base(navigationService)
        {
            eventAggregator.GetEvent<UpdateReadyEvent>().Subscribe(Update);
            Title = "Marinas";
            Marinas = new ObservableCollection<Marina>();
            _dataStore = dataStore;
        }

        public void Update(
         string payload)
        {
            if (payload == "marina")
            {
                Marinas.Clear();
                foreach (var marina in _dataStore.Marinas.OrderBy(_ => _.Name))
                    Marinas.Add(marina);
            }
        }

        public override void OnNavigatedFrom(
            INavigationParameters parameters)
        {
     
        }

        public override void OnNavigatedTo(
            INavigationParameters parameters)
        {
            if (Marinas.Count == 0)
                Update("marina");
        }
    }
}