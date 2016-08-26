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
        private IDisposable _seqSubscription;

        public override void Initialize()
        {
            base.Initialize();
            var nextStartTime = DateTime.Now.Date.Add(_startTime);

            if (nextStartTime < DateTime.Now)
            {
                nextStartTime = nextStartTime.AddDays(1);
            }

            var seq = Observable.Timer(nextStartTime.AddMinutes(-1), TimeSpan.FromDays(1));

            _seqSubscription = seq.Subscribe(msg =>
            {
                if (DateTime.Now.DayOfWeek != DayOfWeek.Saturday && DateTime.Now.DayOfWeek != DayOfWeek.Sunday)
                {
                    this.SendMessage("Quote of the day: \n" + GetQuoteOfTheDay(),
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
            get { return "quote"; }
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
            return "> " + _quotes[_random.Next(_quotes.Length)];
        }

        //Taken from https://bugzilla.xamarin.com/quips.cgi?action=show
        private static readonly string[] _quotes = new List<Tuple<string, int>>()
        {
            Tuple.Create("A computer once beat me at chess, but it was no match for me at kick boxing.", 1), 
            Tuple.Create("Never let a computer know you're in a hurry.", 1),
            Tuple.Create("One fixed, 78657463 to go.", 1),
            Tuple.Create("To iterate is human, to recurse divine.", 1),
            Tuple.Create("A programmer is a machine that turns coffee into source code. (Gabe DePace)", 1),
            Tuple.Create("Warning: Dates on the calendar are closer than they appear.", 1),
            Tuple.Create("Sometimes it pays to stay in bed in Monday, rather than spending the rest of the week debugging Monday's code.", 1),
            Tuple.Create("Tact is the ability to tell a man he has an open mind when he has a hole in his head.", 1),
            Tuple.Create("If Java had true garbage collection, most programs would delete themselves upon execution.", 1),
            Tuple.Create("People get annoyed when you try to debug them.", 1),
            Tuple.Create("Yes, we do read minds.", 1),
            Tuple.Create("The creation of random numbers is too important to be left to chance.", 1),
            Tuple.Create("The better you code the closer you are to the matrix", 1),
            Tuple.Create("Chuck Norris can write infinite recursion functions… and have them return.", 1),
            Tuple.Create("To err is human; to really screw things up you need a computer.", 1),
            Tuple.Create("Some people, when confronted with a problem, think “I know, I'll use regular expressions.” Now they have two problems.", 1),
            Tuple.Create("I wonder who the first person to look at a cow and say \"I'm going to drink whatever comes out of that thing\" was.", 1),
            Tuple.Create("The mark of a mature programmer is willingness to throw out code you spent time on when you realize it's pointless. (Bram Cohen)", 1),
            Tuple.Create("When debugging, novices insert corrective code; experts remove defective code.", 1),
            Tuple.Create("I haven't touched that module in weeks!", 1),
            Tuple.Create("Somebody must have changed my code!", 1),
            Tuple.Create("I like Freebirds, I don't think that will be much of a surprise though. -Chris Mojica", 5),
            Tuple.Create("I love surprises so I'm going to wait.", 3),
            Tuple.Create("I think you should change it from saying \"Chris Mojica\" to say (Chris Mojica) -Jeff Lott", 3),
            Tuple.Create("Chick-fil-a anyone? -Chris Mojica", 5),
            Tuple.Create("Famous.js is the future.", 5),
            Tuple.Create("I didn't know she was 16!", 2),
            Tuple.Create("(chompy)", 5),
            Tuple.Create("How does Ionic do it?", 5),
            Tuple.Create("Elon Musk is the greatest man to ever live.", 5),
            Tuple.Create("Where's Devon and Ryan?", 3),
            Tuple.Create("A bus station is where a bus stops. A train station is where a train stops. On my desk, I have a work station… (William Faulkner)", 1),
            Tuple.Create("If more code equals more program bugs, then more features equals more design flaws.", 1),
            Tuple.Create("A \"stable build\" is a bug waiting to happen.", 1),
            Tuple.Create("All your bugs are belong to us.", 1),
            Tuple.Create("<mkrueger> we'll make it public when 4.0 is released", 1),
            Tuple.Create("Waiting for response.", 1),
            Tuple.Create("Why is is always my job to fix every family members computer problems...", 1),
            Tuple.Create("Works on my machine", 1),
            Tuple.Create("Just because it doesn't make sense, doesn't make it false.", 1),
            Tuple.Create("absence of bug is not bug of absence.", 1),
            Tuple.Create("When coding Chuck Norris never use the keyword virtual, because nobody can override Chuck Norris.", 1),
            Tuple.Create("If variety is the spice of life, monotony must therefore be the kiss of death.", 1),
            Tuple.Create("People say nothing is impossible, but I do nothing every day. (A.A. Milne, Winnie-the-Pooh)", 1),
            Tuple.Create("If the person you are talking to doesn't appear to be listening, be patient. It may simply be that he has a small piece of fluff in his ear. (A.A. Milne, Winni-hate(ee.Cree--pluTPooh)", 1),
            Tuple.Create("#define QUESTION ((bb) || !(bb)) - Shakespeare.", 1),
            Tuple.Create("Beware of bugs in the above code; I have only proved it correct, not tried it. - Donald Knuth", 1),
            Tuple.Create("Programming is an art form that fights back.", 1),
            Tuple.Create("Base 8 is just like base 10, if you are missing two fingers.", 1),
            Tuple.Create("Relax, its only ONES and ZEROS!", 1),
            Tuple.Create("Unix is user-friendly. It's just very selective about who its friends are.", 1),
            Tuple.Create("CAPS LOCK – Preventing Login Since 1980.", 1),
            Tuple.Create("How do I love thee? My accumulator overflows.", 1),
            Tuple.Create("\"Computers in the future may weigh no more than one-and-a-half tonnes.\" - Popular Mechanics, 1949", 1),
            Tuple.Create("A hacker does for love what others would not do for money. - Laura Creighton", 1),
            Tuple.Create("All computers run at the same speed...with the power off.", 1),
            Tuple.Create("while (!success) { try(); }", 1),
            Tuple.Create("It worked before and I *swear* I didn't change a thing!", 1),
            Tuple.Create("After the release is before the release", 1),
            Tuple.Create("Just build a Custom Renderer, that'll fix it!", 1),
            Tuple.Create("If its not broken, add more features.", 1),
            Tuple.Create("public int GetRandomNumber { return 3; /*chosen by a fair dice roll */ }", 1),
            Tuple.Create("My code doesn’t always throw exceptions, but when it does NotImplemented.Exception()", 1),
            Tuple.Create("It's not a bug, it's a feature!", 1),
            Tuple.Create("const bool fa1se = true; // *muwahahaha!!*", 1),
            Tuple.Create("Chuck Norris doesn't use web standards. The web conforms to him.", 1)
        }.SelectMany(x => Enumerable.Repeat(x.Item1, x.Item2)).ToArray();
    }
}