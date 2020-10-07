using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace plinq
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //await RunSync();
            //await RunParallel();
        }

        private static async Task RunSync()
        {
            var http = new HttpClient();
            var rssResponse =
                await http.GetAsync("https://www.standaard.be/rss/section/1f2838d4-99ea-49f0-9102-138784c7ea7c");
            var stream = await rssResponse.Content.ReadAsStreamAsync();
            var document = await XDocument.LoadAsync(stream, LoadOptions.None, CancellationToken.None);
            var sw = Stopwatch.StartNew();

            var articles =
                from link in document.Descendants("guid")
                where link.Attribute("isPermaLink")?.Value == "true"
                select link.Value;
            var count = 0;
            articles
                .Select(link => http.GetAsync(link).Result)
                .Select(result => result.Content.ReadAsStringAsync().Result)
                .ToList()
                .ForEach(text => File.WriteAllTextAsync($"article-{count++}.html", text))
                ;

            sw.Stop();
            Console.WriteLine("Took " + sw.ElapsedMilliseconds);
        }


        static async Task RunParallel()
        {
            var http = new HttpClient();
            var rssResponse =
                await http.GetAsync("https://www.standaard.be/rss/section/1f2838d4-99ea-49f0-9102-138784c7ea7c");
            var stream = await rssResponse.Content.ReadAsStreamAsync();
            var document = await XDocument.LoadAsync(stream, LoadOptions.None, CancellationToken.None);
            var sw = Stopwatch.StartNew();
            
            var articles =
                from link in document.Descendants("guid").AsParallel()
                where link.Attribute("isPermaLink")?.Value == "true"
                select link.Value;
            var count = 0;
            articles
                .Select(link => http.GetAsync(link).Result)
                .Select(result => result.Content.ReadAsStringAsync().Result)
                //.ToList()
                .ForAll(text => File.WriteAllTextAsync($"article-{count++}.html", text))
                ;
            
            sw.Stop();
            Console.WriteLine("Took " + sw.ElapsedMilliseconds);
        }
    }
}