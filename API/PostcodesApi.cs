using System;
using System.Net.Http;
using System.Threading.Tasks;
using Etch.OrchardCore.PostcodeSearch.API.Models;
using Newtonsoft.Json;

namespace Etch.OrchardCore.PostcodeSearch.API
{
    public class PostcodesApi
    {
        private const string URL = "https://api.postcodes.io/postcodes?q=";

        public static async Task<PostcodesApiModel> GetGameData(string postcode)
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
            catch (Exception ex)
            {

            }

            return null;
        }
    }
}
