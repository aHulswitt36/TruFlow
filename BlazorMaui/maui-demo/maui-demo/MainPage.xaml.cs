using Infrastructure;
using Microsoft.Maui.Controls;
using System;
using System.Linq;
using Microsoft.Maui;
using maui_demo.Services;
using Microsoft.Maui.Controls.Xaml;
using System.Threading.Tasks;
using Infrastructure.Models;
using System.Runtime.CompilerServices;
using maui_demo.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace maui_demo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage, IPage
    {
        private readonly IServiceProvider _services;
        public MainPage(IServiceProvider services)
        {
            InitializeComponent();
            _services = services;
        }

        int count = 0;
        private void OnCounterClicked(object sender, EventArgs e)
        {
            count++;
            CounterLabel.Text = $"Current count: {count}";
        }

        async void OnRiversClicked(object sender, EventArgs e)
        {
            var vm = _services.GetRequiredService<RiversViewModel>();
            await (App.Current.MainPage as NavigationPage).PushAsync(new Rivers(vm));
        }
    }
}
