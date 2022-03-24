using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinForms.Services;
using XamarinForms.Views;

namespace XamarinForms
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
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
