namespace Infrastructure
{
    public class UsgsService : IUsgsService
    {
        
        private readonly HttpClient _httpClient;
        public UsgsService(HttpClient client){
            this._httpClient = client;
        }
    }
}