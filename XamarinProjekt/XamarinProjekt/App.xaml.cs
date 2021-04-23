using System;
using TinyIoC;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinProjekt.Models;
using XamarinProjekt.Services;
using XamarinProjekt.Views;

namespace XamarinProjekt
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();
            MonkeyCache.SQLite.Barrel.ApplicationId = "MyApp";

            var container = TinyIoCContainer.Current;
            container.Register<IDataStore<Item>, MockDataStore>();

            MainPage = new AppShell();
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
