using Infrastructure;
using Microsoft.Maui.Controls;
using System;
using System.Linq;
using Microsoft.Maui;
using maui_demo.Services;
using Microsoft.Maui.Controls.Xaml;

namespace maui_demo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage, IPage
    {
        private readonly IUsgsService _usgsService;
        public MainPage()
        {
            InitializeComponent();
            _usgsService = ServiceExtensions.GetService<IUsgsService>();

        }

        int count = 0;
        private void OnCounterClicked(object sender, EventArgs e)
        {
            count++;
            CounterLabel.Text = $"Current count: {count}";
        }

        private void OnRiversClicked(object sender, EventArgs e)
        {
            count++;
            var test = _usgsService.GetSitesForState("OH").Result;
            var hoga = test.Sites.sites.First(s => s.Name.Contains("Cuyahoga"));

            CounterLabel.Text = $"Site: {hoga}";
        }
    }
}
