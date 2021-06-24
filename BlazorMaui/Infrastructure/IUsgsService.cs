using Infrastructure.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure
{
    public interface IUsgsService
    {
        Task<UsgsSites> GetSitesForState(string stateCd);
        Task<bool> GetSiteValues(string siteNumber);
    }
}
