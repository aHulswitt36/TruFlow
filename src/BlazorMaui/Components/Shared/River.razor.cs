using Infrastructure;
using Infrastructure.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Components.Shared
{
    public partial class River : ComponentBase
    {
        [Parameter]
        public string SiteNumber { get; set; }

        [Inject]
        IUsgsService _usgsService { get; set; }

        private List<TimeSeries> riverData;

        private bool isLoading;


        protected override async Task OnInitializedAsync()
        {
            isLoading = true;
            await BuildRiverData();
            //return await base.OnInitializedAsync();
        }

        private async Task BuildRiverData()
        {
            var siteInfo = await _usgsService.GetSiteValues(SiteNumber);
            foreach (var value in siteInfo.Value.TimeSeries)
            {
                var sortedValues = value.Values[0].Value.OrderBy(b => b.DateTime).ToList();

                if (sortedValues == null) return;
                var riverValue = value.Values[0];

                value.Variable.VariableName = value.Variable.VariableName.Split(',')[0];
                //if(sortedValues[0].Value > sortedValues[1].Value)
                //{
                //    riverValue.CalculatedValue = sortedValues[0].Value.ToString() + '↑';
                //}
                //else if (sortedValues[0].Value == sortedValues[1].Value)
                //{
                //    riverValue.CalculatedValue = sortedValues[0].Value.ToString() + '-';
                //}
                //else
                //{
                //    riverValue.CalculatedValue = sortedValues[0].Value.ToString() + '↓';
                //}
            }
            riverData = siteInfo.Value.TimeSeries;
            isLoading = false;
        }

        public void RefreshMe()
        {
            StateHasChanged();
        }
    }
}
