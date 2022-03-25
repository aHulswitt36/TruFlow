using Infrastructure;
using Infrastructure.Models;
using MauiDemoPreview12.Services;
using System.Windows.Input;

namespace MauiDemoPreview12.ViewModels;

public class RiversViewModel : BaseViewModel
{
    private ICommand _stateSelectedCommand;

    private readonly IUsgsService _usgsService;
    private readonly INavigationService _navigationService;

    public RiversViewModel(IUsgsService usgsService, INavigationService navigationService)
    {
        _usgsService = usgsService;
        _navigationService = navigationService;
        IsSiteDataLoaded = false;
        _riverData = new List<TimeSeries>();
    }

    private void SetFavorite(object obj)
    {
        var site = (UsgsSite)obj;
        site.IsFavorite = true;
    }

    private void UnsetFavorite(object obj)
    {
        var site = (UsgsSite)obj;
        site.IsFavorite = false;
    }

    private void OpenRiver(object obj)
    {
        var river = (UsgsSite)obj;
        _navigationService.NavigateToPageAsync<River>(river);
    }

    public List<State> States
    {
        get { return BuildStateList(); }
    }

    public ICommand SetFavoriteCommand => new Command(SetFavorite);
    public ICommand UnsetFavoriteCommand => new Command(UnsetFavorite);
    public ICommand OpenRiverCommand => new Command(OpenRiver);

    State _selectedState;
    public State SelectedState
    {
        get { return _selectedState; }
        set
        {
            if (_selectedState != value)
            {
                _selectedState = value;
                Task.Run(async () => { await GetStateSites(); });
                OnPropertyChanged(nameof(SelectedState));
            }
        }
    }

    List<UsgsSite> _stateSites;
    public List<UsgsSite> StateSites
    {
        get { return _stateSites; }
        set
        {
            if (_stateSites != value)
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

    private bool _isSitesEnabled;
    public bool IsSitesEnabled
    {
        get => _isSitesEnabled;
        set
        {
            if (_isSitesEnabled != value)
            {
                _isSitesEnabled = value;
                OnPropertyChanged("IsSitesEnabled");
            }
        }
    }

    private async Task GetStateSites()
    {
        var sites = await _usgsService.GetSitesForState(_selectedState.Code);
        StateSites = sites.Sites.sites.OrderBy(s => s.Name).ToList();
        IsSitesEnabled = true;
    }

    private async Task GetSiteInfo()
    {
        IsSiteDataLoaded = false;
        RiverData = null;
        var siteInfo = await _usgsService.GetSiteValues(_selectedSite.Number);
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


    private string _photoPath;
    bool _showPhoto;

    public bool ShowPhoto
    {
        get => _showPhoto;
        set
        {
            _showPhoto = value;
            OnPropertyChanged("ShowPhoto");
        }
    }

    public string PhotoPath { get { return _photoPath; } set { if (value != _photoPath) _photoPath = value; OnPropertyChanged("PhotoPath"); } }


    public ICommand CapturePhotoCommand => new Command(TakePicture, () => MediaPicker.IsCaptureSupported);

    private async void TakePicture()
    {
        try
        {
            var photo = await MediaPicker.CapturePhotoAsync();

            await LoadPhotoAsync(photo);

            Console.WriteLine($"CapturePhotoAsync COMPLETED: {PhotoPath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"CapturePhotoAsync THREW: {ex.Message}");
        }
    }

    async Task LoadPhotoAsync(FileResult photo)
    {
        // canceled
        if (photo == null)
        {
            PhotoPath = null;
            return;
        }

        // save the file into local storage
        var newFile = Path.Combine(FileSystem.CacheDirectory, photo.FileName);
        using (var stream = await photo.OpenReadAsync())
        using (var newStream = File.OpenWrite(newFile))
        {
            await stream.CopyToAsync(newStream);
        }

        PhotoPath = newFile;
        ShowPhoto = true;
    }


}
