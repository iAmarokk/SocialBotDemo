using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SocialBotDemo
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var response = await GetString("https://www.kellysubaru.com/used-inventory/index.htm");
            await GetObject(response);
            Console.ReadLine();
        }


        public static async Task<string> GetString(string Url)
        {
            var content = "";
            using (WebClient webClient = new WebClient())
            {
                webClient.Headers[HttpRequestHeader.Accept] = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
                webClient.Headers[HttpRequestHeader.AcceptEncoding] = "br;q=1.0, gzip;q=0.8, *;q=0.1";
                webClient.Headers[HttpRequestHeader.AcceptLanguage] = "ru-RU,ru;q=0.8,en-US;q=0.5,en;q=0.3";
                webClient.Headers[HttpRequestHeader.CacheControl] = "max-age=0";
                webClient.Headers[HttpRequestHeader.UserAgent] = "Mozilla/5.0 (Windows NT 6.1; Win64; x64; rv:47.0) Gecko/20100101 Firefox/47.0";
                content = await webClient.DownloadStringTaskAsync(Url);
            }
            return content;
        }

        public static async Task GetObject(string data)
        {
            HtmlParser domParser = new HtmlParser();
            IHtmlDocument document = await domParser.ParseDocumentAsync(data);

            var items2 = document.QuerySelectorAll("div");
            var desc = document.QuerySelector(".description");
            var subaru = document.QuerySelector("#compareForm > div > div.bd > ul:nth-child(1) > li:nth-child(2) > div.hproduct.auto.subaru");
            var price = subaru.QuerySelector(".value").TextContent;
            var vin = subaru.QuerySelector(".vin").TextContent;
            var urlObject = subaru.QuerySelector(".media");
            var url = urlObject.QuerySelector("a").LastElementChild.GetAttribute("src");



            Console.WriteLine(price);
            Console.WriteLine(vin);
            Console.WriteLine(url);

        }

    }
}
