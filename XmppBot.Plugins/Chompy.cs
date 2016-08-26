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
            if (line.Raw.IndexOf(":chompy:", StringComparison.InvariantCultureIgnoreCase) >= 0 || 
                line.Raw.IndexOf("food", StringComparison.InvariantCultureIgnoreCase) >= 0)
            {
                switch (_random.Next(16))
                {
                    case 0: return ":banana:";
                    case 1: return ":poultry_leg:";
                    case 2: return ":hamburger:";
                    case 3: return ":poop:";
                    case 4: return ":pizza:";
                    case 5: return ":hotdog:";
                    case 6: return ":spaghetti:";
                    case 7: return ":taco:";
                    case 8: return ":burrito:";
                    case 9: return ":icecream:";
                    case 10: return ":cake:";
                    case 11: return ":cookie:";
                    case 12: return ":beer:";
                    case 13: return ":tropical_drink:";
                    case 14: return ":doughnut:";
                    case 15: return ":torchy:";
                }
            }

            return null;
        }

        public override string Name
        {
            get { return "chompy"; }
        }
    }
}