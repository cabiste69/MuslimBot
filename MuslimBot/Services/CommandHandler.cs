using System.Reflection;
using Discord;
using Discord.Addons.Hosting;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MuslimBot.Services
{
    public sealed class CommandHandler : DiscordClientService
    {
        private readonly IServiceProvider _provider;
        private readonly CommandService _service;
        private readonly IConfiguration _config;

        public CommandHandler(DiscordSocketClient client, ILogger<DiscordClientService> logger, IServiceProvider provider, CommandService service, IConfiguration config) : base(client, logger)
        {
            _provider = provider;
            _service = service;
            _config = config;
        }

        // only runs once when the app starts
        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            Client.MessageReceived += OnMessageReceived;
            _service.CommandExecuted += OnCommandExecuted;
            await _service.AddModulesAsync(Assembly.GetEntryAssembly(), _provider);
        }

        private async Task OnMessageReceived(SocketMessage arg)
        {
            if (!(arg is SocketUserMessage message)) return;
            if (message.Source != MessageSource.User) return;

            // Console.WriteLine(message.Content);

            var argPos = 0;
            if (!message.HasStringPrefix(_config["prefix"], ref argPos) && !message.HasMentionPrefix(Client.CurrentUser, ref argPos)) return;

            if(message.Author.Id == 326497071731310592)
            {
                Random random = new();
                if (random.Next(10) == 2)
                {
                    await message.ReplyAsync("fuck you nasro");
                }
            }

            var context = new SocketCommandContext(Client, message);
            await _service.ExecuteAsync(context, argPos, _provider);
        }

        private async Task OnCommandExecuted(Optional<CommandInfo> command, ICommandContext context, IResult result)
        {
            if (command.IsSpecified && !result.IsSuccess) await context.Channel.SendMessageAsync($"Error: {result}");
        }
    }
}