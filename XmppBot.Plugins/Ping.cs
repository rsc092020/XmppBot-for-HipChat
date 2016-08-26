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
    public class Ping : XmppBotPluginBase, IXmppBotPlugin
    {
        private readonly HashSet<string> _aliases = new HashSet<string>(new[]
        {
            "ping",
            "hello"
        }, StringComparer.InvariantCultureIgnoreCase);

        public override string EvaluateEx(ParsedLine line)
        {
            if (!line.IsCommand || !_aliases.Contains(line.Command.ToLower())) return null;

            if (line.Command.Equals("ping", StringComparison.InvariantCultureIgnoreCase))
            {
                return "pong!";
            }
            else if (line.Command.Equals("hello", StringComparison.InvariantCultureIgnoreCase))
            {
                return "world!";
            }

            return "404";
        }

        public override string Name
        {
            get { return "ping"; }
        }
    }
}
