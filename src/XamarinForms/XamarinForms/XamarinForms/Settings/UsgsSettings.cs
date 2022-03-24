using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinForms.Settings
{
    public class UsgsSettings
    {
        public string BaseUrl { get; set; }
    }

    public class BaseSettings
    {
        public UsgsSettings UsgsSettings { get; set; }
    }
}
