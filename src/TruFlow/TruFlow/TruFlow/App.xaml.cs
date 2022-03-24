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
            AppCenter.Start("android=a7075d2c-ea68-4e4a-8466-eccd83b7f32f;" +
                  "ios=2b675344-cbd0-4833-8d76-0d0f1fee3411;",
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
