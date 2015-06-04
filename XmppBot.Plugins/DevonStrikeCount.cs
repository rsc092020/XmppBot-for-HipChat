using System;
using System.ComponentModel.Composition;
using System.Reactive.Linq;
using XmppBot.Common;

namespace XmppBot.Plugins
{
    [Export(typeof(IXmppBotPlugin))]
    public class DevonStrikeCount : XmppBotPluginBase, IXmppBotPlugin
    {
        private int _strikeCount = 20;

        public override string EvaluateEx(ParsedLine line)
        {
            if (line.IsCommand && line.Command.ToLower() == "devonstrike")
            {
                return "Devon strike count at: " + _strikeCount;
            }

            if (line.User.Name == "Devon Gilbert")
            {
                _strikeCount++;

                if (_strikeCount % 5 == 0)
                {

                    string message = string.Format("Devon strike count {0}", _strikeCount);

                    if (_strikeCount > 30)
                    {
                        message = string.Format("WOW! A new strike record! Devon strike count {0}", _strikeCount);
                    }
                    else if (_strikeCount > 50)
                    {
                        message = string.Format("UNSTOPPABLE! Devon strike count {0}!", _strikeCount);
                    }
                    else if (_strikeCount > 70)
                    {
                        message = string.Format("GODLIKE! Devon strike count {0}!", _strikeCount);
                    }
                    else if (_strikeCount > 70)
                    {
                        message = string.Format("Somebody stop him!!! Devon strike count {0}!", _strikeCount);
                    }

                    return message;
                }
            }

            return null;
        }

        public override string Name
        {
            get { return "DevonStrikeCount"; }
        }
    }
}