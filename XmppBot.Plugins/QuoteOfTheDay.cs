using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Reactive.Linq;
using XmppBot.Common;

namespace XmppBot.Plugins
{
    [Export(typeof(IXmppBotPlugin))]
    public class QuoteOfTheDay : XmppBotPluginBase, IXmppBotPlugin
    {
        private readonly HashSet<string> _aliases = new HashSet<string>(new[]
        {
            "quote",
            "quoteoftheday",
            "q"
        }, StringComparer.InvariantCultureIgnoreCase);

        private readonly HashSet<string> _randomAliases = new HashSet<string>(new[]
        {
            "random",
            "rand",
            "r"
        }, StringComparer.InvariantCultureIgnoreCase);

        private readonly TimeSpan _startTime = new TimeSpan(0, 9, 30, 0);
        private static readonly Random _random = new Random();
        private DateTime _currentQuoteDay = DateTime.Today;
        private string _currentQuote = "";

        public QuoteOfTheDay()
        {
            var nextStartTime = DateTime.Now.Date.Add(_startTime);

            if (nextStartTime < DateTime.Now)
            {
                nextStartTime = nextStartTime.AddDays(1);
            }

            var seq = Observable.Timer(nextStartTime.AddMinutes(-1), TimeSpan.FromDays(1));

            seq.Subscribe(msg =>
            {
                this.SendMessage("Quote of the day: \n" + GetQuoteOfTheDay(),
                    GetDefaultRoomJid(), BotMessageType.groupchat);
            });
        }

        public override string EvaluateEx(ParsedLine line)
        {
            if (!line.IsCommand || !_aliases.Contains(line.Command.ToLower())) return null;

            var firstArgument = line.Args.FirstOrDefault();

            if (firstArgument != null && _randomAliases.Contains(firstArgument))
            {
                return GetRandomQuote();
            }

            return GetQuoteOfTheDay();
        }

        public override string Name
        {
            get { return "QuoteOfTheDay"; }
        }

        private string GetQuoteOfTheDay()
        {
            if (string.IsNullOrEmpty(_currentQuote) || !_currentQuoteDay.Equals(DateTime.Today))
            {
                _currentQuote = GetRandomQuote();
                _currentQuoteDay = DateTime.Today;
            }
            return _currentQuote;
        }

        private string GetRandomQuote()
        {
            return _quotes[_random.Next(_quotes.Length)];
        }

        //Taken from https://bugzilla.xamarin.com/quips.cgi?action=show
        private readonly string[] _quotes =
        {
            "A computer once beat me at chess, but it was no match for me at kick boxing.",
            "Never let a computer know you're in a hurry.",
            "One fixed, 78657463 to go.",
            "To iterate is human, to recurse divine.",
            "A programmer is a machine that turns coffee into source code. (Gabe DePace)",
            "Warning: Dates on the calendar are closer than they appear.",
            "Sometimes it pays to stay in bed in Monday, rather than spending the rest of the week debugging Monday's code.",
            "Tact is the ability to tell a man he has an open mind when he has a hole in his head.",
            "If Java had true garbage collection, most programs would delete themselves upon execution.",
            "People get annoyed when you try to debug them.",
            "Yes, we do read minds.",
            "The creation of random numbers is too important to be left to chance.",
            "The better you code the closer you are to the matrix",
            "Chuck Norris can write infinite recursion functions… and have them return.",
            "To err is human; to really screw things up you need a computer.",
            "Some people, when confronted with a problem, think “I know, I'll use regular expressions.” Now they have two problems.",
            "I wonder who the first person to look at a cow and say \"I'm going to drink whatever comes out of that thing\" was.",
            "The mark of a mature programmer is willingness to throw out code you spent time on when you realize it's pointless. (Bram Cohen)",
            "When debugging, novices insert corrective code; experts remove defective code.",
            "I haven't touched that module in weeks!",
            "Somebody must have changed my code!",
            "A bus station is where a bus stops. A train station is where a train stops. On my desk, I have a work station… (William Faulkner)",
            "If more code equals more program bugs, then more features equals more design flaws.",
            "A \"stable build\" is a bug waiting to happen.",
            "All your bugs are belong to us.",
            "<mkrueger> we'll make it public when 4.0 is released",
            "Waiting for response.",
            "Why is is always my job to fix every family members computer problems...",
            "Works on my machine",
            "Just because it doesn't make sense, doesn't make it false.",
            "absence of bug is not bug of absence.",
            "When coding Chuck Norris never use the keyword virtual, because nobody can override Chuck Norris.",
            "If variety is the spice of life, monotony must therefore be the kiss of death.",
            "People say nothing is impossible, but I do nothing every day. (A.A. Milne, Winnie-the-Pooh)",
            "If the person you are talking to doesn't appear to be listening, be patient. It may simply be that he has a small piece of fluff in his ear. (A.A. Milne, Winnie-the-Pooh)",
            "#define QUESTION ((bb) || !(bb)) - Shakespeare.",
            "Beware of bugs in the above code; I have only proved it correct, not tried it. - Donald Knuth",
            "Programming is an art form that fights back.",
            "Base 8 is just like base 10, if you are missing two fingers.",
            "Relax, its only ONES and ZEROS!",
            "Unix is user-friendly. It's just very selective about who its friends are.",
            "CAPS LOCK – Preventing Login Since 1980.",
            "How do I love thee? My accumulator overflows.",
            "\"Computers in the future may weigh no more than one-and-a-half tonnes.\" - Popular Mechanics, 1949",
            "A hacker does for love what others would not do for money. - Laura Creighton",
            "All computers run at the same speed...with the power off.",
            "while (!success) { try(); }",
            "It worked before and I *swear* I didn't change a thing!",
            "After the release is before the release",
            "Just build a Custom Renderer, that'll fix it!",
            "If its not broken, add more features.",
            "public int GetRandomNumber { return 3; /*chosen by a fair dice roll */ }",
            "My code doesn’t always throw exceptions, but when it does NotImplemented.Exception()",
            "It's not a bug, it's a feature!",
            "const bool fa1se = true; // *muwahahaha!!*",
            "Chuck Norris doesn't use web standards. The web conforms to him."
        };
    }
}