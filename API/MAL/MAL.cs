using System.Text.Json;
using Newtonsoft.Json;
using System.Net.Http;
namespace Anime3x3.API.MAL
{
    public static class MAL
    {
        //Using Jikan api wrapper
        private const string BaseURL = "https://api.jikan.moe/v4";
        public enum AnimeType
        {
            tv,
            movie,
            ova,
            special,
            ona,
            music
        }


        public static async Task<SearchResponse.Rootobject> GetAnime(string search, bool sfw = true, params AnimeType[] types)
        {
            string sfwString = "";

            if (sfw == true)
            {
                sfwString = "&sfw=true";
            }

            string? response = await Util.DownloadString(BaseURL + $"/anime?q={search}{TypesToQueryString(types)}{sfwString}&order_by=popularity&limit=1000");
            JsonSerializerSettings settings = new();
            settings.NullValueHandling = NullValueHandling.Ignore;
            if (response != null)
                return JsonConvert.DeserializeObject<SearchResponse.Rootobject>(response, settings);
            else
                return null;
        }


        public static async Task<string> GetRandomAnimeName()
        {
            string S_resp = await Util.DownloadString(BaseURL + $"/random/anime");
            if (!string.IsNullOrEmpty(S_resp))
            {
                var dataRoot = JsonDocument.Parse(S_resp).RootElement.GetProperty("data");

                if (dataRoot.TryGetProperty("title_english", out JsonElement titleEN) && !string.IsNullOrEmpty(titleEN.ToString()))
                {
                    return titleEN.ToString();
                }
                else if (dataRoot.TryGetProperty("title", out JsonElement titleDefault) && !string.IsNullOrEmpty(titleDefault.ToString()))
                {
                    return titleDefault.ToString();
                }
                else if (dataRoot.TryGetProperty("title_japanese", out JsonElement titleJP) && !string.IsNullOrEmpty(titleJP.ToString()))
                {
                    return titleJP.ToString();
                }
            }


            return null;
        }

        private static string TypesToQueryString(params AnimeType[] types)
        {
            string queryString = string.Empty;

            foreach (var item in types)
            {
                queryString += $"&type={item}";

            }

            return queryString;
        }


    }
}
