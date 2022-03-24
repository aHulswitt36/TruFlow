
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using XamarinForms.Models;
using XamarinForms.Settings;

namespace XamarinForms.Services
{
    public interface IUsgsService
    {
        Task<UsgsSites> GetSitesForState(string stateCd);
        Task<UsgsRiver> GetSiteValues(string siteNumber);
    }

    public class UsgsService : IUsgsService
    {
        private readonly HttpClient _httpClient;
        private readonly UsgsSettings _usgsSettings;

        public UsgsService(HttpClient client, UsgsSettings settings)
        {
            _httpClient = client;
            _usgsSettings = settings;
        }

        public async Task<UsgsSites> GetSitesForState(string stateCd)
        {
            try
            {
                var url = $"site/?format=mapper,1.0&stateCd={stateCd}&siteStatus=all&hasDataTypeCd=iv";
                var response = await _httpClient.GetAsync(url);
                var sites = new UsgsSites();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var serializer = new XmlSerializer(typeof(UsgsSites));
                    var el = XElement.Load(await response.Content.ReadAsStreamAsync());
                    sites = (UsgsSites)serializer.Deserialize(el.CreateReader());
                }
                return sites;
            }
            catch (Exception e)
            {
                return new UsgsSites();
            }

        }

        public async Task<UsgsRiver> GetSiteValues(string siteNumber)
        {
            try
            {
                var url = $"iv/?format=json&sites={siteNumber}&siteStatus=all";
                var response = await _httpClient.GetAsync(url);
                var river = new UsgsRiver();
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    throw new Exception($"USGS responded with: {response.StatusCode} and message: {response.ReasonPhrase}");
                }
                river = JsonConvert.DeserializeObject<UsgsRiver>(await response.Content.ReadAsStringAsync());

                return river;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
