using MauiDemo.ViewModels;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;

namespace MauiDemo
{
    public partial class Rivers : ContentPage
    {
        public Rivers(RiversViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;
        }
    }
}