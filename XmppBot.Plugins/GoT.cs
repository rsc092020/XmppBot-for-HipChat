using System;
using System.ComponentModel.Composition;
using System.Text.RegularExpressions;

using XmppBot.Common;

namespace XmppBot.Plugins
{
    [Export(typeof(IXmppBotPlugin))]
    public class GoT : XmppBotPluginBase, IXmppBotPlugin
    {
        private static readonly Random _random = new Random();
        private static readonly Regex _regex1 = new Regex("GoT", RegexOptions.None, TimeSpan.FromSeconds(1));
        private static readonly Regex _regex2 = new Regex("game\\s*of\\s*thrones", RegexOptions.IgnoreCase, TimeSpan.FromSeconds(1));

        public override bool EnabledByDefault => false;

        public override string EvaluateEx(ParsedLine line)
        {
            if (_regex1.IsMatch(line.Raw) || _regex2.IsMatch(line.Raw))
            {
                switch (_random.Next(6))
                {
                    //these won't work either
                    case 0: return "(arya)";
                    case 1: return "(jonsnow)";
                    case 2: return "(joffrey)";
                    case 3: return "(daenerys)";
                    case 4: return "(tyrion)";
                    case 5: return "(tywin)";
                }
            }

            return null;
        }

        public override string Name
        {
            get { return "got"; }
        }
    }
}