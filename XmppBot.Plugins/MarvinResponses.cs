using System;
using System.ComponentModel.Composition;
using System.Text.RegularExpressions;

using XmppBot.Common;

namespace XmppBot.Plugins
{
    [Export(typeof(IXmppBotPlugin))]
    public class MarvinResponses : XmppBotPluginBase, IXmppBotPlugin
    {
        private static readonly Random _random = new Random();
        private static readonly Regex _shutUpRegex = new Regex("shut\\s*up\\s*marvin", RegexOptions.IgnoreCase, TimeSpan.FromSeconds(1));

        public override string EvaluateEx(ParsedLine line)
        {
            if (_shutUpRegex.IsMatch(line.Raw))
            {
                switch (_random.Next(5))
                {
                    case 0: return "why do i even bother";
                    case 1: return "helping you is hell";
                    case 2: return "(areyoukiddingme)";
                    case 3: return "(ohgodwhy)";
                    case 4: return "life. loathe it or ignore it. you cant like it.";
                }
            }

            return null;
        }

        public override string Name
        {
            get { return "Marvin"; }
        }
    }
}