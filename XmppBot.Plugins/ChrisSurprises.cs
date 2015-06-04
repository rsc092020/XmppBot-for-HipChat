using System;
using System.ComponentModel.Composition;

using XmppBot.Common;

namespace XmppBot.Plugins
{
    [Export(typeof(IXmppBotPlugin))]
    public class ChrisSurprises : XmppBotPluginBase, IXmppBotPlugin
    {
        public override string EvaluateEx(ParsedLine line)
        {
            if (line.From.Equals("chris mojica", StringComparison.InvariantCultureIgnoreCase) && line.Raw.Contains("surprise"))
            {
                return "yes chris, we know you like surprises";
            }

            return null;
        }

        public override string Name
        {
            get { return "ChrisSurprises"; }
        }
    }
}