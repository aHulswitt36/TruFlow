using Infrastructure;
using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XamarinForms.ViewModel
{
    public class RiverViewModel : BaseViewModel
    {
        private UsgsSite _site;
        private readonly IUsgsService _usgsService;

        public RiverViewModel(IUsgsService usgsService)
        {
            _usgsService = usgsService;
            IsSiteDataLoaded = false;
        }

        public UsgsSite Site
        {
            get { return _site; }
            set
            {
                if (_site != value)
                {
                    _site = value;
                    Task.Run(async () => { await GetSiteInfo(); });
                    OnPropertyChanged("SelectedSite");
                }
            }
        }

        List<TimeSeries> _riverData;
        public List<TimeSeries> RiverData
        {
            get { return _riverData; }
            set
            {
                if (_riverData != value)
                {
                    _riverData = value;
                    OnPropertyChanged("RiverData");
                }
            }
        }

        bool _isSiteDataLoaded;
        public bool IsSiteDataLoaded
        {
            get { return _isSiteDataLoaded; }
            set
            {
                if (_isSiteDataLoaded != value)
                {
                    _isSiteDataLoaded = value;
                    OnPropertyChanged(nameof(IsSiteDataLoaded));
                }
            }
        }

        #region Navigation Events

        public override async Task OnNavigatingTo(object? parameter)
        {
            if (parameter != null)
                _site = parameter as UsgsSite;
            await GetSiteInfo();
            await base.OnNavigatingTo(parameter);
        }

        #endregion

        private async Task GetSiteInfo()
        {
            IsSiteDataLoaded = false;
            RiverData = null;
            var siteInfo = await _usgsService.GetSiteValues(_site.Number);
            foreach (var value in siteInfo.Value.TimeSeries)
            {
                var sortedValues = value.Values[0].Value.OrderBy(b => b.DateTime).ToList();

                if (sortedValues == null) return;
                var riverValue = value.Values[0];

                value.Variable.VariableName = value.Variable.VariableName.Split(',')[0];
            }
            RiverData = siteInfo.Value.TimeSeries;
            IsSiteDataLoaded = true;
        }
    }
}
