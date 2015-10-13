using System;
using System.ComponentModel.Composition;
using XmppBot.Common;

namespace XmppBot.Plugins
{
    [Export(typeof(IXmppBotPlugin))]
    public class Chompy : XmppBotPluginBase, IXmppBotPlugin
    {
        private static readonly Random _random = new Random();

        public override string EvaluateEx(ParsedLine line)
        {
            if (line.Raw.IndexOf("(chompy)", StringComparison.InvariantCultureIgnoreCase) >= 0 || 
                line.Raw.IndexOf("food", StringComparison.InvariantCultureIgnoreCase) >= 0)
            {
                switch (_random.Next(8))
                {
                    case 0: return "(turkey)";
                    case 1: return "(beer)";
                    case 2: return "(greenbeer)";
                    case 3: return "(poo)";
                    case 4: return "(awyeah)";
                    case 5: return "(cookie)";
                    case 6: return "(forscale)";
                    case 7: return "(taco)";
                }
            }

            return null;
        }

        public override string Name
        {
            get { return "Chompy"; }
        }
    }
}