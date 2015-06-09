using System;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Fizzler.Systems.HtmlAgilityPack;

using HtmlAgilityPack;

using XmppBot.Common;

namespace XmppBot.Plugins
{
    [Export(typeof(IXmppBotPlugin))]
    public class Xkcd : XmppBotPluginBase, IXmppBotPlugin
    {
        public override string EvaluateEx(ParsedLine line)
        {
            try
            {
                if (line.Raw.IndexOf("http://xkcd", StringComparison.InvariantCultureIgnoreCase) >= 0)
                {
                    return "(haha)";
                }

                if (line.IsCommand && line.Command == "xkcd")
                {
                    var html = GetHtml("http://dynamic.xkcd.com/random/comic/").Result;

                    return Format(Scrape(html));
                }
            }
            catch (Exception)
            {
                
            }

            return null;
        }

        private string Format(Tuple<HtmlNode, string> scrape)
        {
            if (scrape == null || scrape.Item1 == null)
            {
                return null;
            }

            return scrape.Item1.Attributes["src"].Value;
        }

        private Tuple<HtmlNode, string> Scrape(string html)
        {
            if (string.IsNullOrWhiteSpace(html))
            {
                return null;
            }

            var document = new HtmlDocument();
            document.LoadHtml(html);
            var image = document.DocumentNode.QuerySelectorAll("#comic img").FirstOrDefault();
            var title = document.DocumentNode.QuerySelectorAll("#ctitle").FirstOrDefault();

            return Tuple.Create(image, title != null ? title.InnerText : "");

        }

        private async Task<string> GetHtml(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return content;
                }
            }

            return null;
        }

        public override string Name
        {
            get { return "Xkcd"; }
        }
    }
}