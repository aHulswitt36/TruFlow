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

            isLoading = false;
        }
    }
}
