using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Infrastructure.Models
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

    public class UsgsSite
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
    }
}
