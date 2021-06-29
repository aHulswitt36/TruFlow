using Components.Shared;
using Infrastructure;
using Infrastructure.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Components.Pages
{
    public partial class Rivers : ComponentBase
    {
        [Inject]
        IUsgsService _usgsService { get; set; }
        
        private List<State> states;
        private List<UsgsSite> sites;
        private bool selectedState = false;
        private string selectedSiteNumber;
        private bool selectedSite = false;

        protected River ChildComponent;
        

        protected override async Task OnInitializedAsync()
        {
            states = BuildStateList();
            //return base.OnInitializedAsync();
        }

        private List<State> BuildStateList()
        {
            return new List<State>
            {
                new State("Alabama","AL"),
                new State("Alaska","AK"),
                new State("Arizona","AZ"),
                new State("Arkansas","AR"),
                new State("California","CA"),
                new State("Colorado","CO"),
                new State("Connecticut","CT"),
                new State("Delaware","DE"),
                new State("Florida","FL"),
                new State("Hawaii","HI"),
                new State("Idaho","ID"),
                new State("Illinois","IL"),
                new State("Indiana","IN"),
                new State("Iowa","IA"),
                new State("Kansas","KS"),
                new State("Kentucky","KY"),
                new State("Louisiana","LA"),
                new State("Maine","ME"),
                new State("Maryland","MD"),
                new State("Massachusetts","MA"),
                new State("Michigan","MI"),
                new State("Minnesota","MN"),
                new State("Mississippi","MS"),
                new State("Missouri","MO"),
                new State("Montana","MT"),
                new State("Nebraska","NE"),
                new State("Nevada","NV"),
                new State("New Hampshire","NH"),
                new State("New Jersey","NJ"),
                new State("New Mexico","NM"),
                new State("New York","NY"),
                new State("North Carolina","NC"),
                new State("North Dakota","ND"),
                new State("Ohio","OH"),
                new State("Oklahoma","OK"),
                new State("Oregon","OR"),
                new State("Pennsylvania","PA"),
                new State("Rhode Island","RI"),
                new State("South Carolina","SC"),
                new State("South Dakota","SD"),
                new State("Tennessee","TN"),
                new State("Texas","TX"),
                new State("Utah","UT"),
                new State("Vermont","VT"),
                new State("Virginia","VA"),
                new State("Washington","WA"),
                new State("West Virginia","WV"),
                new State("Wisconsin","WI"),
                new State("Wyoming","WY")
            };
        }

        async Task OnStateSelection(object value, string name)
        {
            if (!string.IsNullOrEmpty(value.ToString()))
            {
                selectedState = true;
                sites = null;
                selectedSiteNumber = null;
            }

            var usgsSites = await _usgsService.GetSitesForState(value.ToString().ToLower());
            sites = usgsSites.Sites.sites.OrderBy(s => s.Name).ToList();
        }

        async Task OnSiteSelection(object value, string name)
        {
            selectedSite = false;
            StateHasChanged();
            selectedSite = true;
            selectedSiteNumber = value.ToString();
        }
    }
}
