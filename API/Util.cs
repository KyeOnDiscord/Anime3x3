
namespace Anime3x3.API
{
    public static class Util
    {
        private static HttpClient httpClient = new HttpClient();

        /// <summary>
        /// Downloads a string, returns null if it's unsuccessful.
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static async Task<string> DownloadString(string url) => await httpClient.GetStringAsync(url);
    }
}
