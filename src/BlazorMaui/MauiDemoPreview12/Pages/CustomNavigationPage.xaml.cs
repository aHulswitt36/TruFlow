using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;


namespace MauiDemoPreview12.Pages
{
    public partial class CustomNavigationPage : NavigationPage
    {
        public CustomNavigationPage(IServiceProvider services) : base(new MainPage(services))
        {
            InitializeComponent();
        }
    }
}