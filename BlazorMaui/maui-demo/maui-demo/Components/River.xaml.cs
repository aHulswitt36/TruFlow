using Infrastructure.Models;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;
using System;
using System.Collections.Generic;

namespace maui_demo.Components
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class River : ContentView
    {
        public List<TimeSeries> RiverData
        {
            get { return (List<TimeSeries>) GetValue(RiverDataProperty); }
            set { SetValue(RiverDataProperty, value); }
        }
        public River()
        {
            InitializeComponent();
        }

        public static readonly BindableProperty RiverDataProperty = BindableProperty.Create(
            nameof(RiverData),
            typeof(List<TimeSeries>),
            typeof(River),
            null,
            BindingMode.TwoWay,
            propertyChanged: OnRiverDataChanged);

        static void OnRiverDataChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (River)bindable;
            control.RiverData = (List<TimeSeries>) newValue;
        }
    }
}
