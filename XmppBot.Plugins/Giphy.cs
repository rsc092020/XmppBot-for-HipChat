using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

using Newtonsoft.Json;

using XmppBot.Common;

namespace XmppBot.Plugins
{
    [Export(typeof(IXmppBotPlugin))]
    public class Giphy : XmppBotPluginBase, IXmppBotPlugin
    {
        private readonly HashSet<string> _aliases = new HashSet<string>(new[]
        {
            "giphy",
            "gihpy",
            "giffy"
        }, StringComparer.InvariantCultureIgnoreCase);

        public override string EvaluateEx(ParsedLine line)
        {
            if (!line.IsCommand || !_aliases.Contains(line.Command.ToLower())) return null;

            var index = 0;

            var args = line.Args.AsEnumerable();

            if (line.Args.Length > 1 && int.TryParse(line.Args[0], out index))
            {
                args = args.Skip(1);
            }

            var query = string.Join(" ", args);

            Task.Factory.StartNew(async () =>
            {
                using (var client = new HttpClient())
                {
                    var uriString = string.Format("http://api.giphy.com/v1/gifs/search?q={0}&limit=1&offset={1}&api_key=dc6zaTOxFJmzC", HttpUtility.UrlEncode(query), index);

                    var response = await client.GetAsync(new Uri(uriString));

                    if (response.IsSuccessStatusCode)
                    {
                        var results = JsonConvert.DeserializeObject<GiphyDatas>(await response.Content.ReadAsStringAsync());

                        var data = results != null && results.data != null ? results.data.FirstOrDefault() : null;
                        var any = results != null && results.data != null && results.data.Any();

                        if (data != null && data.images != null && data.images.original != null)
                        {
                            this.SendMessage(data.images.original.url, line.From, BotMessageType.groupchat);
                        }
                        else if (!any)
                        {
                            this.SendMessage("I no find gif...", line.From, BotMessageType.groupchat);
                        }
                    }
                }
            });

            return null;
        }

        public override string Name
        {
            get { return "User Actions"; }
        }

        private class GiphyDatas
        {
            public IEnumerable<GiphyData> data { get; set; }

            public class GiphyData
            {
                public GiphyImages images { get; set; }

                public class GiphyImages
                {
                    public Image original { get; set; }

                    public class Image
                    {
                        public string url { get; set; }
                    }
                }
            }
        }
    }
}
