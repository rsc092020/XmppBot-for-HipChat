using System;
using System.ComponentModel.Composition;
using System.Linq;
using System.Reactive.Linq;

using XmppBot.Common;

namespace XmppBot.Plugins
{
    [Export(typeof(IXmppBotPlugin))]
    public class StandupSchedule : XmppBotPluginBase, IXmppBotPlugin
    {
        private readonly TimeSpan _startTime = new TimeSpan(0, 9, 30, 0);
        private IDisposable _seqSubscription;

        public override bool EnabledByDefault => false;

        public override void Initialize()
        {
            base.Initialize();
            var nextStartTime = DateTime.Now.Date.Add(_startTime);

            if (nextStartTime < DateTime.Now)
            {
                nextStartTime = nextStartTime.AddDays(1);
            }

            var seq = Observable.Timer(nextStartTime.AddMinutes(-2), TimeSpan.FromDays(1));

            _seqSubscription =  seq.Subscribe(msg =>
            {
                if (DateTime.Now.DayOfWeek != DayOfWeek.Saturday && DateTime.Now.DayOfWeek != DayOfWeek.Sunday)
                {
                    this.SendMessage("Standup starts in 2 minutes guys. Be there or be square!",
                        GetDefaultRoomJid(), BotMessageType.groupchat);
                }
            });
        }

        public override void Disable()
        {
            base.Disable();
            _seqSubscription.Dispose();
        }

        public override string EvaluateEx(ParsedLine line)
        {
            return null;
        }

        public override string Name
        {
            get { return "standup-schedule"; }
        }
    }
}