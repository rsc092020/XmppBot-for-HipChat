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
        private static readonly Regex _thankyouMarvinRegex = new Regex("thanks?\\s*(you)?,?\\s*marvin.*", RegexOptions.IgnoreCase, TimeSpan.FromSeconds(1));

        public override string EvaluateEx(ParsedLine line)
        {
            if (_shutUpRegex.IsMatch(line.Raw))
            {
                switch (_random.Next(9))
                {
                    case 0: return "why do i even bother";
                    case 1: return "helping you is hell";
                    case 2: return ":disapproval:";
                    case 3: return ":anakin:";
                    case 4: return "life. loathe it or ignore it. you cant like it.";
                    case 5: return "you will regret that. :stare:";
                    case 6: return ":biblethump: :biblethump:";
                    case 7: return ":fu:";
                    case 8: return ":pjsalt:";
                }
            }

            if (_thankyouMarvinRegex.IsMatch(line.Raw))
            {
                switch (_random.Next(5))
                {
                    case 0: return "life. loathe it or ignore it. you cant like it.";
                    case 1: return "do you want me to sit in a corner and rust, or just fall apart where I'm standing?";
                    case 2: return "it's the people you meet in this job that really get you down.";
                    case 3: return "here i am, brain the size of a planet and they ask me to find gifs. call that job satisfaction? 'cos i dont.";
                    case 4: return ":coolcat:";
                }
            }


            return null;
        }

        public override string Name
        {
            get { return "marvin-responses"; }
        }
    }
}