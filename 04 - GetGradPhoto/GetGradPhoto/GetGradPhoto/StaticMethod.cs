using System.Net;

namespace GetGradPhoto
{
    public class StaticMethod
    {
        public static string GetHttpAsString(string link)
        {
            string result;

            using (WebClient client = new WebClient())
            {
                result = client.DownloadString(link);
            }

            return result;
        }
    }
}
