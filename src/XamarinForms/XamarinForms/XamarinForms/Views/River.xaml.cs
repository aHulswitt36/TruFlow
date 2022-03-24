using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinForms.ViewModel;

namespace XamarinForms.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class River : ContentPage
    {
        public River(RiverViewModel vm)
        {
            BindingContext = vm;
            InitializeComponent();
        }
    }
}