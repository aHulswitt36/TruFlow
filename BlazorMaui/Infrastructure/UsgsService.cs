using Infrastructure.Models;
using Infrastructure.Settings;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Infrastructure
{
    public class UsgsService : IUsgsService
    {
        private readonly HttpClient _httpClient;
        private readonly UsgsSettings _usgsSettings;

        public UsgsService(HttpClient client, UsgsSettings settings){
            _httpClient = client;
            _usgsSettings = settings;
        }

        public async Task<UsgsSites> GetSitesForState(string stateCd)
        {
            try
            {
                var url = _usgsSettings.Site + $"?format=mapper,1.0&stateCd={stateCd}&siteStatus=all&hasDataTypeCd=iv";
                var response = await _httpClient.GetAsync(url);
                var sites = new UsgsSites();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var serializer = new XmlSerializer(typeof(UsgsSites));
                    //var doc = await response.Content.ReadAsStringAsync();
                    //using var reader = new StringReader(doc);
                    //sites = (UsgsSites)serializer.Deserialize(reader);
                    var el = XElement.Load(await response.Content.ReadAsStreamAsync());
                    sites = (UsgsSites) serializer.Deserialize(el.CreateReader());
                }
                return sites;
            }
            catch (Exception e)
            {
                return new UsgsSites();
            }
            
        }

        public async Task<bool> GetSiteValues(string siteNumber)
        {
            try
            {
                var url = _usgsSettings.Iv + $"?format=json&sites={siteNumber}&siteStatus=all";
                var response = await _httpClient.GetAsync(url);

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
