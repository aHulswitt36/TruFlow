using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Xml.Serialization;

namespace XamarinForms.Models
{
    [XmlRoot("mapper")]
    public class UsgsSites
    {
        [XmlElement("sites")]
        public Sites Sites { get; set; }
        [XmlElement("colocated_sites")]
        public ColocatedSites ColocatedSites { get; set; }
    }

    [XmlRoot("sites")]
    public class Sites
    {
        [XmlElement("site")]
        public List<UsgsSite> sites = new List<UsgsSite>();
    }

    [XmlRoot("colocated_sites")]
    public class ColocatedSites
    {
        [XmlElement("site")]
        public List<UsgsSite> sites = new List<UsgsSite>();
    }

    public class UsgsSite : INotifyPropertyChanged
    {
        [XmlAttribute("sno")]
        public string Number { get; set; }
        [XmlAttribute("sna")]
        public string Name { get; set; }
        [XmlAttribute("cat")]
        public string Category { get; set; }
        [XmlAttribute("lat")]
        public decimal Latitude { get; set; }
        [XmlAttribute("lng")]
        public decimal Longitude { get; set; }
        [XmlAttribute("agc")]
        public string AGC { get; set; }

        public string RiverName
        {
            get
            {
                if (Name.IndexOf(" at ") != -1)
                    return Name.Substring(0, Name.IndexOf(" at "));
                else if (Name.IndexOf(" near ") != -1)
                    return Name.Substring(0, Name.IndexOf(" near "));
                else if (Name.IndexOf(" above ") != -1)
                    return Name.Substring(0, Name.IndexOf(" above "));
                else
                    return Name;
            }
        }

        public string Location
        {
            get
            {
                if (Name.IndexOf(" at ") != -1)
                    return Name.Substring(Name.IndexOf(" at ") + 4);
                else if (Name.IndexOf(" near ") != -1)
                    return Name.Substring(Name.IndexOf(" near ") + 6);
                else if (Name.IndexOf(" above ") != -1)
                    return Name.Substring(Name.IndexOf(" above ") + 7);
                else
                    return Name;
            }
        }

        private bool _isFavorite;
        public bool IsFavorite
        {
            get { return _isFavorite; }
            set
            {
                if (_isFavorite != value)
                {
                    _isFavorite = value;
                    OnPropertyChanged("IsFavorite");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
