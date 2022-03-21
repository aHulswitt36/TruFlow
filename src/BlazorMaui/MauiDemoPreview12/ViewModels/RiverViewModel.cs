using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiDemoPreview12.ViewModels
{
    public class RiverViewModel : BaseViewModel
    {
        private UsgsSite _siteData;

        public RiverViewModel()
        {
        }

        #region Navigation Events
        public override Task OnNavigatingTo(object? parameter)
        {
            if(parameter != null)
                _siteData = parameter as UsgsSite;
            return base.OnNavigatingTo(parameter);
        }

        #endregion
    }
}
