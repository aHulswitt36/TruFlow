using MauiDemoPreview12.Services;
using MauiDemoPreview12.ViewModels;

namespace MauiDemoPreview12
{
    public partial class MainPage : ContentPage
    {
        private readonly INavigationService _navigationService;

        public MainPage(INavigationService navigationService)
        {
            InitializeComponent();
            _navigationService = navigationService;
        }
        int count = 0;
        private void OnCounterClicked(object sender, EventArgs e)
        {
            count++;
            //CounterLabel.Text = $"Current count: {count}";

           // SemanticScreenReader.Announce(CounterLabel.Text);
        }
        async void OnRiversClicked(object sender, EventArgs e)
        {
            await _navigationService.NavigateToPageAsync<Rivers>();
        }
    }

}
