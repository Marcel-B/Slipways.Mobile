using Prism.Events;
using Prism.Navigation;
using Slipways.Mobile.Events;
using Slipways.Mobile.Helpers;
using System.Collections.ObjectModel;

namespace Slipways.Mobile.ViewModels
{
    public abstract class ListViewModel<T> : ViewModelBase
    {
        public ListViewModel(
            string dataType,
            IEventAggregator eventAggregator,
            INavigationService navigationService) : base(navigationService)
        {
            DataType = dataType;
            Data = new ObservableCollection<T>();
            eventAggregator.GetEvent<UpdateReadyEvent<T>>().Subscribe(Update);
        }

        public string DataType { get; }

        private ObservableCollection<T> _data;
        public ObservableCollection<T> Data
        {
            get => _data;
            set => SetProperty(ref _data, value);
        }

        public virtual void Update(
            DataUpdateEventArgs<T> args)
        {
            //if (args.Type.ToLower() == DataType)
            //{
            //    Data.Clear();
            //    foreach (var data in args.Data)
            //        Data.Add(data);
            //}
        }

        public override abstract void OnNavigatedFrom(INavigationParameters parameters);
        public override abstract void OnNavigatedTo(INavigationParameters parameters);
    }
}
