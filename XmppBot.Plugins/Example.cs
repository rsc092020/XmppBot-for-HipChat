using System;
using System.ComponentModel.Composition;
using System.Linq;

using XmppBot.Common;

namespace XmppBot.Plugins
{
    [Export(typeof(IXmppBotPlugin))]
    public class Example : XmppBotPluginBase, IXmppBotPlugin
    {
        private const string MattsId = "mspradley";

        public override string EvaluateEx(ParsedLine line)
        {
            if (!line.IsCommand) return string.Empty;

            switch (line.Command.ToLower())
            {
                case "smack":
                    var person = line.User.Id == MattsId ? "themself" : (line.Args.FirstOrDefault() ?? "themself");

                    return String.Format("{0} smacks {1} around with a large trout", line.User.Name, person);
                case "hug":
                    return String.Format("{0} hugs {1}", line.User.Name, line.Args.FirstOrDefault() ?? "themself");

                default:
                    return null;
            }
        }

        public override string Name
        {
            get { return "example"; }
        }
    }
}
