using Infrastructure;
using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MauiDemoPreview12.ViewModels;
public class RiverViewModel : BaseViewModel
{
    private UsgsSite _site;
    private readonly IUsgsService _usgsService;

    public ICommand CapturePhotoCommand { get; }
    private string _photoPath;
    bool _showPhoto;

    public bool ShowPhoto
    {
        get => _showPhoto;
        set {
            _showPhoto = value;
            OnPropertyChanged("ShowPhoto");
        }
    }

    public string PhotoPath { get { return _photoPath; } set { if(value != _photoPath) _photoPath = value; OnPropertyChanged("PhotoPath"); } }
        
    public RiverViewModel(IUsgsService usgsService)
    {
        _usgsService = usgsService;
        IsSiteDataLoaded = false;

        CapturePhotoCommand = new Command(TakePicture, () => MediaPicker.IsCaptureSupported);
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
        if(parameter != null)
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

