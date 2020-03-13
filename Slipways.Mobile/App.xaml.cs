﻿using Prism.DryIoc;
using Prism.Ioc;
using Slipways.Mobile.Data;
using Prism;
using System;
using Xamarin.Forms;
using Slipways.Mobile.ViewModels;
using Slipways.Mobile.Views;
using Slipways.Mobile.Contracts;
using Slipways.Mobile.Services;
using GraphQL.Client.Abstractions;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using Slipways.Mobile.Data.Repositories;
using Prism.Events;
using Slipways.Mobile.Events;
using Prism.Services;

namespace Slipways.Mobile
{
    public partial class App : PrismApplication
    {
        public App() : this(null)
        {
        }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        protected async override void OnInitialized()
        {
            InitializeComponent();
            var context = Container.Resolve<IDataContext>();
            await NavigationService.NavigateAsync("NavigationPage/MainPage");
            var eventAggregator = Container.Resolve<IEventAggregator>();
            var e = eventAggregator.GetEvent<InitializationReadyEvent>();
            var initializeReady = await context.Initialize();
        }

        protected override void RegisterTypes(
            IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterInstance<IGraphQLClient>(new GraphQLHttpClient((options) =>
            {
                options.EndPoint = new Uri("https://data.slipways.de/graphql");
                options.JsonSerializer = new NewtonsoftJsonSerializer();
            }));

            containerRegistry.RegisterSingleton<IDataContext, DataContext>();
            containerRegistry.RegisterInstance<IEventAggregator>(new EventAggregator());
            containerRegistry.Register<IGraphQLService, GraphQLService>();
            containerRegistry.Register<IManufacturerRepository, ManufacturerRepository>();
            containerRegistry.Register<IServiceRepository, ServiceRepository>();
            containerRegistry.Register<IMarinaRepository, MarinaRepository>();
            containerRegistry.Register<ISlipwayRepository, SlipwayRepository>();
            containerRegistry.Register<IWaterRepository, WaterRepository>();
            containerRegistry.Register<IStationRepository, StationRepository>();
            containerRegistry.Register<IExtraRepository, ExtraRepository>();
            containerRegistry.Register<IUpdateService, UpdateService>();

            containerRegistry.RegisterSingleton<IRepositoryWrapper, RepositoryWrapper>();
            containerRegistry.RegisterSingleton<IDataStore, DataStore>();

            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<WaterPage, WaterPageViewModel>();
            containerRegistry.RegisterForNavigation<SlipwaysListPage, SlipwaysListPageViewModel>();
            containerRegistry.RegisterForNavigation<InfoPage, InfoPageViewModel>();
            containerRegistry.RegisterForNavigation<SlipwayDetails, SlipwayDetailsViewModel>();
            containerRegistry.RegisterForNavigation<MarinaPage, MarinaPageViewModel>();
            containerRegistry.RegisterForNavigation<ServicePage, ServicePageViewModel>();
            containerRegistry.RegisterForNavigation<LevelPage, LevelPageViewModel>();
            containerRegistry.RegisterForNavigation<MapPage, MapPageViewModel>();
        }
    }
}
