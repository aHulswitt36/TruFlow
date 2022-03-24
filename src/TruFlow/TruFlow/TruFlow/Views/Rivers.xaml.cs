using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruFlow.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TruFlow.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Rivers : ContentPage
    {
        public Rivers(RiversViewModel vm)
        {
            BindingContext = vm;
            InitializeComponent();
        }
    }
}
