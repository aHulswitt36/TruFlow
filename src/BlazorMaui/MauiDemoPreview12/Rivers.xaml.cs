using MauiDemoPreview12.ViewModels;

namespace MauiDemoPreview12
{
    public partial class Rivers : ContentPage
    {
        public Rivers(RiversViewModel vm)
        {
            BindingContext = vm;
            InitializeComponent();
        }
    }
}
