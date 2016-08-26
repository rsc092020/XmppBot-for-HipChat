using System;
using System.ComponentModel.Composition;
using XmppBot.Common;

namespace XmppBot.Plugins
{
    [Export(typeof(IXmppBotPlugin))]
    public class Derp : XmppBotPluginBase, IXmppBotPlugin
    {
        private static readonly Random _random = new Random();

        private static int _counter = 50;

        public override string EvaluateEx(ParsedLine line)
        {
            _counter--;

            var chance = _random.Next(Math.Max(1, _counter));

            if (chance == 0)
            {
                _counter += 50;

                switch (_random.Next(7))
                {
                    case 0: return ":badman:";
                    case 1: return ":keepo:";
                    case 2: return ":facepalm:";
                    case 3: return ":dumb:";
                    case 4: return ":disapproval:";
                    case 5: return ":crickets:";
                    case 6: return ":pjsalt:";
                }
            }

            return null;
        }

        public override string Name
        {
            get { return "derp"; }
        }
    }
}