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
        private static readonly Regex _thankyouRegex = new Regex("thank\\s*you,?\\s*marvin", RegexOptions.IgnoreCase, TimeSpan.FromSeconds(1));

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

            if (_thankyouRegex.IsMatch(line.Raw))
            {
                switch (_random.Next(4))
                {
                    case 0: return "life. loathe it or ignore it. you cant like it.";
                    case 1: return "do you want me to sit in a corner and rust, or just fall apart where I'm standing?";
                    case 2: return "it's the people you meet in this job that really get you down.";
                    case 3: return "here i am, brain the size of a planet and they as me to find gifs. call that job satisfaction? 'cos i dont.";
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