using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using System;
using TruFlow.Services;
using TruFlow.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TruFlow
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            Startup.Init();
            var nav = Startup.ServiceProvider.GetService<INavigationService>();
            MainPage = new NavigationPage();
            nav.NavigateToPageAsync<Rivers>();
        }

        protected override void OnStart()
        {
            AppCenter.Start("android=75e58358-1253-445b-a408-34819fe867a4;",
                  typeof(Analytics), typeof(Crashes));
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
