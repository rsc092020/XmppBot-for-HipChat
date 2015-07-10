using System;
using System.ComponentModel.Composition;
using System.Linq;

using XmppBot.Common;

namespace XmppBot.Plugins
{
    [Export(typeof(IXmppBotPlugin))]
    public class Example : XmppBotPluginBase, IXmppBotPlugin
    {
        private const string MattsId = "92448_673247";

        public override string EvaluateEx(ParsedLine line)
        {
            if (!line.IsCommand) return string.Empty;

            switch (line.Command.ToLower())
            {
                case "smack":
                    if (line.User.Id != MattsId)
                    {
                        return String.Format("{0} smacks {1} around with a large trout", line.User.Name,
                            line.Args.FirstOrDefault() ?? "themself");
                    }

                    return null;
                case "hug":
                    return String.Format("{0} hugs {1}", line.User.Name, line.Args.FirstOrDefault() ?? "themself");

                case "help":
                    return String.Format("Right now the only commands I know are !smack [thing] and !hug [thing].");

                default:
                    return null;
            }
        }

        public override string Name
        {
            get { return "User Actions"; }
        }
    }
}
