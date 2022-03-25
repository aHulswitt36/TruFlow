using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Essentials;

namespace MauiBlazor.Pages
{
    public partial class TakePicture : ComponentBase
    {
        [Inject]
        IJSRuntime JSRuntime { get; set; }

        private string _photoPath;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (firstRender)
                await RequestMedia();
        }

        async Task RequestMedia()
        {
            // Try just wait??
            await Task.Delay(5000);

            try
            {

                await JSRuntime.InvokeVoidAsync("requestMedia");

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private async Task CapturePicture()
        {
            var photo = await MediaPicker.CapturePhotoAsync();
            await LoadPhotoAsync(photo);
        }

        private async Task LoadPhotoAsync(FileResult photo)
        {
            if(photo == null)
            {
                _photoPath = null;
                return;
            }

            var newFile = Path.Combine(FileSystem.CacheDirectory, photo.FileName);
            using (var stream = await photo.OpenReadAsync())
            using (var newStream = File.OpenWrite(newFile))
                await stream.CopyToAsync(newStream);

            _photoPath = newFile;
        }
    }
}
