using MauiDemoPreview12.ViewModels;

namespace MauiDemoPreview12
{
    public partial class River : ContentPage
    {
        public River(RiverViewModel vm)
        {
            BindingContext = vm;
            InitializeComponent();

            NavigationPage.SetHasNavigationBar(this, false);
        }
    }
}
