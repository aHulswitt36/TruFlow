using maui_demo.ViewModels;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;

namespace maui_demo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Rivers : ContentPage
    {
        public Rivers(RiversViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;            
        }
    }
}
