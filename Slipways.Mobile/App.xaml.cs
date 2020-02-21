using Slipways.Mobile.Data;
using System;
using Xamarin.Forms;

namespace Slipways.Mobile
{
    public partial class App : Application
    {
        private static SlipwaysDatabase database;

        public static SlipwaysDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new SlipwaysDatabase();
                }
                return database;
            }
        }

        public App()
        {
            InitializeComponent();

            var basePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            Console.WriteLine(basePath);
            MainPage = new NavigationPage(new MainPage { Title = "slipways.de" });
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
