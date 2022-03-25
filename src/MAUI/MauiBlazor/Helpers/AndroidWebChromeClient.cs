#if ANDROID
using Android.Webkit;
#endif
using Microsoft.AspNetCore.Components.WebView.Maui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiBlazor.Helpers
{
#if ANDROID
    public class MauiWebChromeClient : WebChromeClient
    {
        public override void OnPermissionRequest(PermissionRequest request)
        {
            request.Grant(request.GetResources());
        }
    }

    public class MauiBlazorWebViewHandler : BlazorWebViewHandler
    {
        protected override WebChromeClient GetWebChromeClient()
        {
            return new MauiWebChromeClient();
        }
    }
#endif
}
