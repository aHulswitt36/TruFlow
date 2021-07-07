using MauiDemo.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui.Controls;
using System;

namespace MauiDemo
{
    public partial class MainPage : ContentPage
    {
        private readonly IServiceProvider _serviceProvider;
        public MainPage(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
        }

        int count = 0;
        private void OnCounterClicked(object sender, EventArgs e)
        {
            count++;
            CounterLabel.Text = $"Current count: {count}";
        }


        async void OnRiversClicked(object sender, EventArgs e)
        {
            var vm = _serviceProvider.GetRequiredService<RiversViewModel>();
            await (App.Current.MainPage as NavigationPage).PushAsync(new Rivers(vm));
        }
    }
}
