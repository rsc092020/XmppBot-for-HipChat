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
            if (line.Raw.IndexOf("surprise", StringComparison.InvariantCultureIgnoreCase) +
                line.Raw.IndexOf("suprise", StringComparison.InvariantCultureIgnoreCase) > 0)
            {
                return "surprises, chris likes surprises!";
            }

            return null;
        }

        public override string Name
        {
            get { return "ChrisSurprises"; }
        }
    }
}