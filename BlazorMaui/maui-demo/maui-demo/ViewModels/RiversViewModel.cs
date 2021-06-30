using Infrastructure;
using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace maui_demo.ViewModels
{
    public class RiversViewModel : BaseViewModel
    {
        private readonly IUsgsService _usgsService;
        public RiversViewModel(IUsgsService usgsService)
        {
            _usgsService = usgsService;
        }
        public List<State> States
        {
            get { return BuildStateList(); }
        }

        State _selectedState;
        public State SelectedState
        {
            get { return _selectedState; }
            set
            {
                if(_selectedState != value)
                {
                    _selectedState = value;
                    OnPropertyChanged("SelectedState");
                }
            }
        }

        List<UsgsSite> _stateSites;
        public List<UsgsSite> StateSites
        {
            get { return _stateSites; }
            set
            {
                if(_stateSites != value)
                {
                    _stateSites = value;
                    OnPropertyChanged("StateSites");
                }
            }
        }

        UsgsSite _selectedSite;
        public UsgsSite SelectedSite
        {
            get { return _selectedSite; }
            set
            {
                if (_selectedSite != value)
                {
                    _selectedSite = value;
                    OnPropertyChanged("SelectedSite");
                }
            }
        }

        private async void StatePicker_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            var sites = await _usgsService.GetSitesForState(_selectedState.Code);
            StateSites = sites.Sites.sites;
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
    }
}
