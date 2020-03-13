using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Prism.Events;
using Prism.Mvvm;
using Prism.Navigation;
using Slipways.Mobile.Events;
using Xamarin.Forms;

namespace Slipways.Mobile.ViewModels
{
    public class DataUpdateEventArgs<T>
    {
        public IEnumerable<T> Data;
        public string Type;
    }
    public abstract class ViewModelBase<T> : BindableBase, INavigationAware, IDestructible
    {
        protected string ViewType { get; }
        private string _title;
        public string Title { get => _title; set => SetProperty(ref _title, value); }
        private ObservableCollection<T> _data;
        public ObservableCollection<T> Data
        {
            get => _data;
            set => SetProperty(ref _data, value);
        }


        protected INavigationService NavigationService { get; private set; }

        public ViewModelBase(
            string viewType,
            IEventAggregator eventAggregator,
            INavigationService navigationService)
        {
            ViewType = viewType.ToLower();
            eventAggregator.GetEvent<UpdateReadyEvent<T>>().Subscribe(Update);
            NavigationService = navigationService;
            Data = new ObservableCollection<T>();
        }

        public void Destroy()
        {
        }

        public virtual void Update(
            DataUpdateEventArgs<T> args)
        {
            if (args.Type.ToLower() == ViewType)
            {
                Data.Clear();
                foreach (var data in args.Data)
                     Data.Add(data);
            }
        }

        public abstract void OnNavigatedFrom(
            INavigationParameters parameters);

        public abstract void OnNavigatedTo(
            INavigationParameters parameters);
    }
}
