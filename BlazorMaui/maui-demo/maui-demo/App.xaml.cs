using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Controls.PlatformConfiguration.WindowsSpecific;
using System;
using Application = Microsoft.Maui.Controls.Application;

namespace maui_demo
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

        //protected override Window CreateWindow(IActivationState activationState)
        //{
        //    //Microsoft.Maui.Controls.Compatibility.Forms.Init(activationState);
        //    On<Windows>().SetImageDirectory("Assets");
        //    return new Window(new NavigationPage(new MainPage()));
        //}
    }
}
