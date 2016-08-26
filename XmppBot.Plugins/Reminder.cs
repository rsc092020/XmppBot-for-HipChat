using System;
using System.ComponentModel.Composition;
using System.Linq;
using System.Reactive.Linq;
using XmppBot.Common;

namespace XmppBot.Plugins
{
    /// <summary>
    /// Adds a command to tell the bot to remind you of something at a specified time
    /// </summary>
    [Export(typeof(IXmppBotPlugin))]
    public class Reminder : XmppBotPluginBase, IXmppBotPlugin
    {
        public override string EvaluateEx(ParsedLine line)
        {
            if(!line.IsCommand || line.Command.ToLower() != "reminder")
            {
                return null;
            }

            const string help = "!reminder [time] [message]";

            // Verify we have enough arguments
            if(line.Args.Length < 2)
            {
                return help;
            }

            DateTimeOffset time;
            
            // Parse the arguments
            if (!DateTimeOffset.TryParse(line.Args[0], out time))
            {
                var time2 = TimeSpan.MaxValue;
                if (TryParseTime(line.Args[0], out time2))
                {
                    time = DateTime.Now.Add(time2);
                }
                else
                {
                    return help;
                }
            }

            // We want anything entered after the time to be included in the reminder
            string message = string.Join(" ", line.Args.Skip(1));

            // Create an sequence that fires off single value at a specified time
            // and transform that value into the reminder message
            IObservable<string> seq = Observable.Timer(time).Select(l => message);

            seq.Subscribe((msg) => { 
                this.SendMessage(msg, line.User.Id, BotMessageType.chat); 
            });

            // Add a start message
            return string.Format("Will do - I'll remind you at {0}.", time);
        }

        private bool TryParseTime(string s, out TimeSpan time2)
        {
            int val;

            if (s.EndsWith("m") && int.TryParse(s.Substring(0, s.IndexOf("m")), out val))
            {
                time2 = TimeSpan.FromMinutes(val);
                return true;
            }
            
            if (s.EndsWith("h") && int.TryParse(s.Substring(0, s.IndexOf("h")), out val))
            {
                time2 = TimeSpan.FromHours(val);
                return true;
            }
            
            if (s.EndsWith("d") && int.TryParse(s.Substring(0, s.IndexOf("d")), out val))
            {
                time2 = TimeSpan.FromDays(val);
                return true;
            }

            time2 = TimeSpan.MinValue;
            return false;
        }

        public override string Name
        {
            get { return "reminder"; }
        }
    }
}