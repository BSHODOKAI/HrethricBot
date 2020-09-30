using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace HrethricBot
{
    class Program
    {
        static void Main(string[] args)
            => new Program().MainAsync().GetAwaiter().GetResult();

        private DiscordSocketClient _client;
        private IConfiguration _config;

        public async Task MainAsync()
        {

            _client = new DiscordSocketClient();
            _config = BuildConfig();

            _client.Log += Log;

            _client.MessageReceived += MessageReceived;

            await _client.LoginAsync(TokenType.Bot, _config["token"]);

            await _client.StartAsync();

            await Task.Delay(-1);

        }

        private IConfiguration BuildConfig()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("config.json")
                .Build();
            return builder;
        }

        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }

        private async Task MessageReceived(SocketMessage message)
        {
            if (message.Author.Username.Equals("Wulf"))
            {
                var connectedServers = _client.Guilds;
                var serverToSendMessage = connectedServers.First(x => x.Name.Equals("Thunder Billies"));
                var channelToSendMessage = serverToSendMessage.Channels.First(x => x.Name.Equals("complaining-about-hrethrics-complaining"));

                await serverToSendMessage.GetTextChannel(channelToSendMessage.Id)
                                         .SendMessageAsync($"Hrethric may have complained in channel: #{message.Channel.Name}");

            }
            if (message.Content.ToLower().Contains("hrethric") && !message.Channel.Name.Contains("hrethric"))
            {
                var connectedServers = _client.Guilds;
;
                foreach (var server in connectedServers)
                {
                    if (server.Name.Equals("Thunder Billies") ||
                        server.Name.Equals("schmeebsisawesome"))
                    {
                        var channelToSendMessage = server.Channels.First(x => x.Name.Equals("complaining-about-hrethrics-complaining"));
                        await server.GetTextChannel(channelToSendMessage.Id)
                                    .SendMessageAsync($"Someone may have mentioned Hrethric (Which he might complain about) in: ${message.Channel.Name}");
                    }
                }
            }
        }
    }
}