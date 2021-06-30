using Infrastructure;
using Microsoft.Maui.Controls;
using System;
using System.Linq;
using Microsoft.Maui;
using maui_demo.Services;
using Microsoft.Maui.Controls.Xaml;
using System.Threading.Tasks;
using Infrastructure.Models;

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
            var hoga = new UsgsSite();

            Task.Run(async () =>
            {
                var test = await _usgsService.GetSitesForState("OH");
                hoga = test.Sites.sites.First(s => s.Name.Contains("Cuyahoga"));
            }).Wait();

            CounterLabel.Text = $"Site: {hoga}";

        }
    }
}
