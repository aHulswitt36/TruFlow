using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Settings
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
