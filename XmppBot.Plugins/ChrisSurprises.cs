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
            const string message = "surprises, chris likes surprises!";

            if ((line.Raw.IndexOf("surprise", StringComparison.InvariantCultureIgnoreCase) >= 0 ||
                line.Raw.IndexOf("suprise", StringComparison.InvariantCultureIgnoreCase) >= 0) &&
                line.Raw != message)
            {
                return message;
            }

            return null;
        }

        public override string Name
        {
            get { return "chris-surprises"; }
        }
    }
}