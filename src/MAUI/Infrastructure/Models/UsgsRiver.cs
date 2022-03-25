using System;
using System.Collections.Generic;

using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Infrastructure.Models
{

    public partial class UsgsRiver
    {
        //[JsonProperty("name")]
        //public string Name { get; set; }

        //[JsonProperty("declaredType")]
        //public string DeclaredType { get; set; }

        //[JsonProperty("scope")]
        //public string Scope { get; set; }

        [JsonProperty("value")]
        public UsgsRiverValue Value { get; set; }
            
        //[JsonProperty("nil")]
        //public bool Nil { get; set; }

        //[JsonProperty("globalScope")]
        //public bool GlobalScope { get; set; }

        //[JsonProperty("typeSubstituted")]
        //public bool TypeSubstituted { get; set; }
    }

    public partial class UsgsRiverValue
    {
        //[JsonProperty("queryInfo")]
        //public QueryInfo QueryInfo { get; set; }

        [JsonProperty("timeSeries")]
        public List<TimeSeries> TimeSeries { get; set; }
    }

    //public partial class QueryInfo
    //{
    //    [JsonProperty("queryURL")]
    //    public Uri QueryUrl { get; set; }

    //    [JsonProperty("criteria")]
    //    public Criteria Criteria { get; set; }

    //    [JsonProperty("note")]
    //    public List<Note> Note { get; set; }
    //}

    //public partial class Criteria
    //{
    //    [JsonProperty("locationParam")]
    //    public string LocationParam { get; set; }

    //    [JsonProperty("variableParam")]
    //    public string VariableParam { get; set; }

    //    [JsonProperty("parameter")]
    //    public List<object> Parameter { get; set; }
    //}

    //public partial class Note
    //{
    //    [JsonProperty("value")]
    //    public string Value { get; set; }

    //    [JsonProperty("title")]
    //    public string Title { get; set; }
    //}

    public partial class TimeSeries
    {
        [JsonProperty("sourceInfo")]
        public SourceInfo SourceInfo { get; set; }

        [JsonProperty("variable")]
        public Variable Variable { get; set; }

        [JsonProperty("values")]
        public List<TimeSeriesValue> Values { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public partial class SourceInfo
    {
        [JsonProperty("siteName")]
        public string SiteName { get; set; }

        [JsonProperty("siteCode")]
        public List<SiteCode> SiteCode { get; set; }

        [JsonProperty("timeZoneInfo")]
        public TimeZoneInfo TimeZoneInfo { get; set; }

        [JsonProperty("geoLocation")]
        public GeoLocation GeoLocation { get; set; }

        [JsonProperty("note")]
        public List<object> Note { get; set; }

        [JsonProperty("siteType")]
        public List<object> SiteType { get; set; }

        [JsonProperty("siteProperty")]
        public List<SiteProperty> SiteProperty { get; set; }
    }

    public partial class GeoLocation
    {
        [JsonProperty("geogLocation")]
        public GeogLocation GeogLocation { get; set; }

        [JsonProperty("localSiteXY")]
        public List<object> LocalSiteXy { get; set; }
    }

    public partial class GeogLocation
    {
        [JsonProperty("srs")]
        public string Srs { get; set; }

        [JsonProperty("latitude")]
        public double Latitude { get; set; }

        [JsonProperty("longitude")]
        public double Longitude { get; set; }
    }

    public partial class SiteCode
    {
        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("network")]
        public string Network { get; set; }

        [JsonProperty("agencyCode")]
        public string AgencyCode { get; set; }
    }

    public partial class SiteProperty
    {
        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public partial class TimeZoneInfo
    {
        [JsonProperty("defaultTimeZone")]
        public TimeZone DefaultTimeZone { get; set; }

        [JsonProperty("daylightSavingsTimeZone")]
        public TimeZone DaylightSavingsTimeZone { get; set; }

        [JsonProperty("siteUsesDaylightSavingsTime")]
        public bool SiteUsesDaylightSavingsTime { get; set; }
    }

    public partial class TimeZone
    {
        [JsonProperty("zoneOffset")]
        public string ZoneOffset { get; set; }

        [JsonProperty("zoneAbbreviation")]
        public string ZoneAbbreviation { get; set; }
    }

    public partial class TimeSeriesValue
    {
        [JsonProperty("value")]
        public List<ValueValue> Value { get; set; }

        public string CalculatedValue { get; set; }

        //[JsonProperty("qualifier")]
        //public List<Qualifier> Qualifier { get; set; }

        //[JsonProperty("qualityControlLevel")]
        //public List<object> QualityControlLevel { get; set; }

        //[JsonProperty("method")]
        //public List<Method> Method { get; set; }

        //[JsonProperty("source")]
        //public List<object> Source { get; set; }

        //[JsonProperty("offset")]
        //public List<object> Offset { get; set; }

        //[JsonProperty("sample")]
        //public List<object> Sample { get; set; }

        //[JsonProperty("censorCode")]
        //public List<object> CensorCode { get; set; }
    }

    public partial class Method
    {
        [JsonProperty("methodDescription")]
        public string MethodDescription { get; set; }

        [JsonProperty("methodID")]
        public long MethodId { get; set; }
    }

    public partial class Qualifier
    {
        [JsonProperty("qualifierCode")]
        public string QualifierCode { get; set; }

        [JsonProperty("qualifierDescription")]
        public string QualifierDescription { get; set; }

        [JsonProperty("qualifierID")]
        public long QualifierId { get; set; }

        [JsonProperty("network")]
        public string Network { get; set; }

        [JsonProperty("vocabulary")]
        public string Vocabulary { get; set; }
    }

    public partial class ValueValue
    {
        [JsonProperty("value")]
        public decimal Value { get; set; }

        //[JsonProperty("qualifiers")]
        //public List<string> Qualifiers { get; set; }

        [JsonProperty("dateTime")]
        public DateTimeOffset DateTime { get; set; }
    }

    public partial class Variable
    {
        //[JsonProperty("variableCode")]
        //public List<VariableCode> VariableCode { get; set; }

        [JsonProperty("variableName")]
        public string VariableName { get; set; }

        [JsonProperty("variableDescription")]
        public string VariableDescription { get; set; }

        //[JsonProperty("valueType")]
        //public string ValueType { get; set; }

        [JsonProperty("unit")]
        public Unit Unit { get; set; }

        //[JsonProperty("options")]
        //public Options Options { get; set; }

        //[JsonProperty("note")]
        //public List<object> Note { get; set; }

        //[JsonProperty("noDataValue")]
        //public long NoDataValue { get; set; }

        //[JsonProperty("variableProperty")]
        //public List<object> VariableProperty { get; set; }

        //[JsonProperty("oid")]
        //public long Oid { get; set; }
        public string DisplayValue { 
            get 
            {
                return $"{VariableName}({Unit.UnitCode})";
            } 
        }
    }

    //public partial class Options
    //{
    //    [JsonProperty("option")]
    //    public List<Option> Option { get; set; }
    //}

    //public partial class Option
    //{
    //    [JsonProperty("name")]
    //    public string Name { get; set; }

    //    [JsonProperty("optionCode")]
    //    public string OptionCode { get; set; }
    //}

    public partial class Unit
    {
        [JsonProperty("unitCode")]
        public string UnitCode { get; set; }
    }

    //public partial class VariableCode
    //{
    //    [JsonProperty("value")]
    //    public string Value { get; set; }

    //    [JsonProperty("network")]
    //    public string Network { get; set; }

    //    [JsonProperty("vocabulary")]
    //    public string Vocabulary { get; set; }

    //    [JsonProperty("variableID")]
    //    public long VariableId { get; set; }

    //    [JsonProperty("default")]
    //    public bool Default { get; set; }
    //}

    
}

