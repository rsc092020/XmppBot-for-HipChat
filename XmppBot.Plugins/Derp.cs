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
                    case 0: return "(derp)";
                    case 1: return "(borat)";
                    case 2: return "(bicepleft)(bumble)(bicepright)";
                    case 3: return "(dumb)";
                    case 4: return "(disapproval)";
                    case 5: return "(grumpycat)";
                    case 6: return "(okay)";
                }
            }

            return null;
        }

        public override string Name
        {
            get { return "Derp"; }
        }
    }
}