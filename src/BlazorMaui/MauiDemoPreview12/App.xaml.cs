using MauiDemoPreview12.Services;

namespace MauiDemoPreview12
{
    public partial class App : Application
    {
        public App(INavigationService navigationService)
        {
            InitializeComponent();

            MainPage = new NavigationPage();
            navigationService.NavigateToPageAsync<Rivers>();
        }
    }
}
