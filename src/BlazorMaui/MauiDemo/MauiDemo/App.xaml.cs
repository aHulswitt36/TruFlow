using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.PlatformConfiguration.WindowsSpecific;
using System;
using Application = Microsoft.Maui.Controls.Application;

namespace MauiDemo
{
    public partial class App : Application
    {
        public IServiceProvider Services { get; }
        public App(IServiceProvider services)
        {
            InitializeComponent();
            Services = services;

            MainPage = Services.GetRequiredService<Microsoft.Maui.Controls.Page>();
        }

        //protected override IWindow CreateWindow(IActivationState activationState)
        //{
        //    this.On<Microsoft.Maui.Controls.PlatformConfiguration.Windows>()
        //        .SetImageDirectory("Assets");

        //    return new Window(Services.GetRequiredService<Microsoft.Maui.Controls.Page>());
        //}
    }
}
