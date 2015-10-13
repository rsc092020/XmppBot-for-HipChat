using System;
using System.ComponentModel.Composition;
using System.Linq;
using XmppBot.Common;

namespace XmppBot.Plugins
{
    [Export(typeof(IXmppBotPlugin))]
    public class Kenneth : XmppBotPluginBase, IXmppBotPlugin
    {
        private static readonly DateTime _whenToStart = new DateTime(2015, 10, 20);

        private static readonly Random _random = new Random();

        public override string EvaluateEx(ParsedLine line)
        {
            if (line.IsCommand || DateTime.Now < _whenToStart)
            {
                return null;
            }

            var tokens = line.Raw.ToLower().Split(" \r\n\t".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).ToList();

            if ((tokens.Contains("kenneth") || tokens.Contains("ken")) &&
                _random.Next(1) == 0)
            {
                return "yea, i miss kenneth too :'(";
            }

            return null;
        }

        public override string Name
        {
            get { return "Kenneth"; }
        }
    }
}