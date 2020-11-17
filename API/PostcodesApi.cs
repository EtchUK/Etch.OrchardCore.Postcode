using System;
using System.Net.Http;
using System.Threading.Tasks;
using Castle.Core.Logging;
using Etch.OrchardCore.Postcode.API.Models;
using Newtonsoft.Json;

namespace Etch.OrchardCore.Postcode.API
{
    public class PostcodesApi
    {
        #region Constants

        private const string URL = "https://api.postcodes.io/postcodes?q=";

        #endregion

        #region Dependencies

        public ILogger Logger { get; set; } = new NullLogger();

        #endregion

        public async Task<PostcodesApiModel> GetGameData(string postcode)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var response = await client.GetAsync(URL + postcode);

                    if (response != null && response.IsSuccessStatusCode)
                    {
                        var jsonString = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<PostcodesApiModel>(jsonString);
                    }
                }
            }
            catch (Exception e)
            {
                Logger.Error(string.Format("{0}, Error retrieving data from API.", e));
            }

            return null;
        }
    }
}
