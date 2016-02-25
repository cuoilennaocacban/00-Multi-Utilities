using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace GetGradPhoto
{
    class Program
    {
        static void Main(string[] args)
        {
            ObservableCollection<ImageModel> imageLinkList = new ObservableCollection<ImageModel>();

            string startLink = "http://uit.edu.vn/photos/index.php?/category/31/start-";

            for (int i = 0; i < 480; i=i+48)
            {
                Console.WriteLine("--------------------------------------------------");
                Console.WriteLine("Get link page: " + i);
                string getLink = startLink + i;
                string tmp = StaticMethod.GetHttpAsString(getLink);

                AddLink(tmp, imageLinkList);
            }

            WebClient client = new WebClient();

            for (int i = 0; i < imageLinkList.Count; i++)
            {
                Console.WriteLine("Download file: " + imageLinkList[i].fileName);
                client.DownloadFile(imageLinkList[i].downloadLink, imageLinkList[i].fileName);
            }
        }

        public static void AddLink(string html, ObservableCollection<ImageModel> list)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(html);

            var liNode =
                doc.DocumentNode.Descendants("li")
                .Where(
                        d =>
                            d.InnerText.Contains("DSC"));


            foreach (HtmlNode node in liNode)
            {
                string value = node.Descendants("a").FirstOrDefault().Attributes["href"].Value;

                var temp = value.Split('/');
                value = temp[0] + "/" + temp[1] + "/";

                Console.WriteLine("New image added: " + value);

                //http://uit.edu.vn/photos/action.php?id=6130&part=e&download
                ImageModel nM = new ImageModel
                {
                    fileName = temp[1] + ".jpg",
                    link = "http://uit.edu.vn/photos/" + value,
                    downloadLink = "http://uit.edu.vn/photos/action.php?id=" + temp[1] + "&part=e&download"
                };

                list.Add(nM);
            }
        }
    }
}
