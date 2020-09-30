using System;
using System.IO;
using System.Threading.Tasks;
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

            _client.Log += Log;

            _client.MessageReceived += MessageReceived;

            await _client.LoginAsync(TokenType.Bot, "NzYwNjg0MjczNTMxMDkzMDQ1.X3Pobw.103yR5hOFgWJAb1pbUyNxxzJnl4");

            await _client.StartAsync();

            await Task.Delay(-1);

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
                foreach (var server in connectedServers)
                {
                    if (server.Name.Equals("Thunder Billies"))
                    {
                        var channels = server.Channels;
                        foreach (var channel in channels)
                        {
                            if (channel.Name.Equals("complaining-about-hrethrics-complaining"))
                            {
                                await server.GetTextChannel(channel.Id).SendMessageAsync($"Hrethric may have complained in channel: #{message.Channel.Name}!");
                            }
                        }
                    }
                }
            }
            if (message.Content.ToLower().Contains("hrethric") && !message.Channel.Name.Contains("hrethric"))
            {
                var connectedServers = _client.Guilds;
                foreach (var server in connectedServers)
                {
                    if (server.Name.Equals("Thunder Billies") ||
                        server.Name.Equals("schmeebsisawesome"))
                    {
                        var channels = server.Channels;
                        foreach (var channel in channels)
                        {
                            if (channel.Name.Equals("complaining-about-hrethrics-complaining"))
                            {
                                await server.GetTextChannel(channel.Id).SendMessageAsync($"Someone may have mentioned Hrethric (Which he might complain about) in: #{message.Channel.Name}!");
                            }
                        }
                    }
                }

            }
        }
    }
}