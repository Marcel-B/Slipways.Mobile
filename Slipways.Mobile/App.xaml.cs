using Prism.DryIoc;
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
using Slipways.Mobile.Data.Models;
using System.Linq;

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
            await NavigationService.NavigateAsync("NavigationPage/MainPage");
        }

        protected override void RegisterTypes(
            IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterInstance<IGraphQLClient>(new GraphQLHttpClient((options) =>
            {
                options.EndPoint = new Uri("https://data.slipways.de/graphql");
                options.JsonSerializer = new NewtonsoftJsonSerializer();
            }));
            containerRegistry.RegisterSingleton<ISlipwaysDatabase, SlipwaysDatabase>();
            containerRegistry.Register<IGraphQLService, GraphQLService>();
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<WaterPage, WaterPageViewModel>();
            containerRegistry.RegisterForNavigation<SlipwaysListPage, SlipwaysListPageViewModel>();
            containerRegistry.RegisterForNavigation<InfoPage, InfoPageViewModel>();
            containerRegistry.RegisterForNavigation<SlipwayDetails, SlipwayDetailsViewModel>();
            containerRegistry.RegisterSingleton<IDataStore, DataStore>();
        }
    }
}
