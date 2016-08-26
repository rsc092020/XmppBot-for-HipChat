using System;
using System.ComponentModel.Composition;
using XmppBot.Common;

namespace XmppBot.Plugins
{
    [Export(typeof(IXmppBotPlugin))]
    public class Exceptions : XmppBotPluginBase, IXmppBotPlugin
    {
        private static readonly Random _random = new Random();

        public override string EvaluateEx(ParsedLine line)
        {
            if (line.Raw.IndexOf("exception", StringComparison.InvariantCultureIgnoreCase) >= 0)
            {
                if (_random.Next(0, 4) == 0)
                {
                    return "Exceptions, Gotta catch them all :pokeball:";
                }
            }

            return null;
        }

        public override string Name
        {
            get { return "Exceptions"; }
        }
    }
}