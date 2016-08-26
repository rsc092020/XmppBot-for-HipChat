using System;
using System.ComponentModel.Composition;
using System.Linq;

using XmppBot.Common;

namespace XmppBot.Plugins
{
    [Export(typeof(IXmppBotPlugin))]
    public class DevonStrikeCount : XmppBotPluginBase, IXmppBotPlugin
    {
        private const string DevonsId = "dgilbert";
        private ulong _strikeCount = 20;

        public override bool EnabledByDefault => false;

        public override string EvaluateEx(ParsedLine line)
        {
            if (line.User.Id == DevonsId && line.User.Name.IndexOf("devon", StringComparison.InvariantCultureIgnoreCase) < 0)
            {
                _strikeCount += _strikeCount;
            }

            if (line.User.Id == DevonsId && line.Raw.Contains("strike"))
            {
                _strikeCount += _strikeCount;
            }

            if (line.IsCommand && line.Command.ToLower() == "devonstrike")
            {
                if (line.User.Id != DevonsId)
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
                    return "Nice try devon :dumb:";
                }
            }

            if (line.User.Id == DevonsId)
            {
                _strikeCount++;

                if (_strikeCount % 20 == 0)
                {
                    var message = string.Format("Devon strike count {0}", _strikeCount);

                    if (_strikeCount > 50 && _strikeCount < 100)
                    {
                        message = string.Format("WOW! A new strike record! Devon strike count {0}", _strikeCount);
                    }
                    else if (_strikeCount < 200)
                    {
                        message = string.Format("UNSTOPPABLE! Devon strike count {0}!", _strikeCount);
                    }
                    else if (_strikeCount < 300)
                    {
                        message = string.Format("GODLIKE! Devon strike count {0}!", _strikeCount);
                    }
                    else if (_strikeCount < 400)
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
            get { return "devon-strike"; }
        }
    }
}