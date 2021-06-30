using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace maui_demo.Behaviors
{
    public class PickerBehaviors : Behavior<Picker>
    {
        protected override void OnAttachedTo(Picker picker)
        {
            picker.SelectedIndexChanged += OnSelectedIndexChange;
            base.OnAttachedTo(picker);
        }

        async void OnSelectedIndexChange(object sender, EventArgs e)
        {

        }
    }
}
