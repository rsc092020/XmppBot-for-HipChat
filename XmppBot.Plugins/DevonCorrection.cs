﻿using System;
using System.ComponentModel.Composition;
using System.Reactive.Linq;
using XmppBot.Common;

namespace XmppBot.Plugins
{
    [Export(typeof(IXmppBotPlugin))]
    public class DevonCorrection : XmppBotPluginBase, IXmppBotPlugin
    {
        private const string Correction = "Dilbert";
        private static readonly Random _random = new Random();

        public override bool EnabledByDefault => false;

        public override string EvaluateEx(ParsedLine line)
        {
            if (line.IsCommand)
            {
                return null;
            }

            if (line.Raw.ToLower().Contains("devon") &&
                !line.Raw.ToLower().StartsWith("devon strike count") &&
                _random.Next(4) == 0)
            {
                return "I think you mean " + Correction;
            }

            return null;
        }

        public override string Name
        {
            get { return "devon-correction"; }
        }
    }
}