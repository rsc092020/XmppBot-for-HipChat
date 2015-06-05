using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

using XmppBot.Common;

namespace XmppBot.Plugins
{
    [Export(typeof(IXmppBotPlugin))]
    public class MacBuildServer : XmppBotPluginBase, IXmppBotPlugin
    {
        private static readonly IEnumerable<string> _ips;
        private static string _currentIp;

        static MacBuildServer()
        {
            var subdomains = new[]
            {
                //im sure there are more we can try here.
                "10.3.1"
            };

            var ips = from s in subdomains
                from b in Enumerable.Range(0, 256)
                select string.Format("{0}.{1}", s, b);

            _ips = ips.ToList();
        }

        public override string EvaluateEx(ParsedLine line)
        {
            if (!line.IsCommand || !line.Command.Equals("findteamcity", StringComparison.InvariantCultureIgnoreCase))
            {
                return null;
            }

            Task.Factory.StartNew(() => FindIpAddress(line));

            return "Alright, I'm looking for the mac team city server. I'll let you know when I find it.";
        }

        public override string Name
        {
            get { return "MacBuildServer"; }
        }

        private async Task FindIpAddress(ParsedLine line)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromSeconds(5);

                    if (!string.IsNullOrWhiteSpace(_currentIp))
                    {
                        var isTeamCity = await DoesIpResolveToTeamCity(client, _currentIp);

                        if (isTeamCity)
                        {
                            PrintIp(_currentIp, line);
                            return;
                        }
                        else
                        {
                            _currentIp = null;
                        }
                    }


                    Parallel.ForEach(_ips, (ip, ls) =>
                    {
                        var closedClient = client;
                        Bot.log.Debug("Trying " + ip);
                        var isTeamCity = DoesIpResolveToTeamCity(closedClient, ip).Result;
                        Bot.log.Debug("Received " + ip);
                        if (isTeamCity)
                        {
                            PrintIp(ip, line);
                            _currentIp = ip;
                            ls.Stop();
                        }
                    });

                    if (string.IsNullOrWhiteSpace(_currentIp))
                    {
                        this.SendMessage("Hmmm. I couldn't find the mac team city server... (shrug)", line.From, BotMessageType.groupchat);
                    }
                }
            }
            catch (Exception)
            {
                this.SendMessage("Something happened and I couldn't find the mac team city server.", line.From, BotMessageType.groupchat);
            }
            
        }

        private void PrintIp(string ip, ParsedLine line)
        {
            this.SendMessage(string.Format("I think I found the mac team city server. {0}", GetTeamCityUrl(ip)),
                line.From, BotMessageType.groupchat);
        }

        private async Task<bool> DoesIpResolveToTeamCity(HttpClient client, string ip)
        {
            try
            {
                var respone = await client.GetAsync(new Uri(GetTeamCityUrl(ip)));

                return respone.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private static string GetTeamCityUrl(string ip)
        {
            return string.Format("http://{0}:8111/login.html", ip);
        }
    }
}