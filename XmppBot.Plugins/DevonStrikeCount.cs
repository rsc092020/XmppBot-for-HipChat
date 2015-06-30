using System;
using System.ComponentModel.Composition;
using System.Linq;
using System.Reactive.Linq;
using XmppBot.Common;

namespace XmppBot.Plugins
{
    [Export(typeof(IXmppBotPlugin))]
    public class DevonStrikeCount : XmppBotPluginBase, IXmppBotPlugin
    {
        private ulong _strikeCount = 20;

        public override string EvaluateEx(ParsedLine line)
        {
            if (line.IsCommand && line.Command.ToLower() == "devonstrike")
            {
                if (line.User.Name.IndexOf("devon", StringComparison.InvariantCultureIgnoreCase) < 0)
                {
                    var first = line.Args.FirstOrDefault();

                    if (first != null &&
                        (first.Equals("add", StringComparison.InvariantCultureIgnoreCase) ||
                         first.Equals("+", StringComparison.InvariantCultureIgnoreCase)))
                    {
                        _strikeCount ++;
                    }

                    if (first != null &&
                        first.Equals("reset", StringComparison.InvariantCultureIgnoreCase) &&
                        line.From.IndexOf("devon", StringComparison.InvariantCultureIgnoreCase) < 0)
                    {
                        _strikeCount = 0;
                    }

                    if (first != null &&
                        first.Equals("set", StringComparison.InvariantCultureIgnoreCase) &&
                        line.From.IndexOf("devon", StringComparison.InvariantCultureIgnoreCase) < 0)
                    {
                        var count = line.Args.Skip(1).FirstOrDefault();
                        ulong lCount;

                        if (ulong.TryParse(count, out lCount))
                        {
                            _strikeCount = lCount;
                        }
                    }

                    return "Devon strike count at: " + _strikeCount;
                }
                else
                {
                    _strikeCount += _strikeCount * 100;
                    return "Nice try devon (derp)";
                }
            }

            if (line.User.Name == "Devon Gilbert")
            {
                _strikeCount++;

                if (_strikeCount % 5 == 0)
                {
                    var message = string.Format("Devon strike count {0}", _strikeCount);

                    if (_strikeCount > 30 && _strikeCount < 40)
                    {
                        message = string.Format("WOW! A new strike record! Devon strike count {0}", _strikeCount);
                    }
                    else if (_strikeCount < 50)
                    {
                        message = string.Format("UNSTOPPABLE! Devon strike count {0}!", _strikeCount);
                    }
                    else if (_strikeCount < 60)
                    {
                        message = string.Format("GODLIKE! Devon strike count {0}!", _strikeCount);
                    }
                    else if (_strikeCount < 70)
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