using Discord;
using Discord.Commands;
using MuslimBot.Services;

namespace MuslimBot.Modules
{
    public sealed class General : ModuleBase<SocketCommandContext>
    {
        private readonly IPrayer _prayerTime;
        public General(IPrayer prayerTime)
        {
            _prayerTime = prayerTime;
        }

        public async Task PingAsync()
        {
            await Context.Channel.TriggerTypingAsync();
            await Context.Channel.SendMessageAsync("Pong!");
        }

        [Command("PrayTime")]
        [Alias("PrayTime", "Pray Time", "PrayerTime", "Prayer Time", "time")]
        public async Task GetPrayerTime([Remainder] string state)
        {
            await Context.Channel.TriggerTypingAsync();
            List<EmbedFieldBuilder> response;
            try
            {
                response = await _prayerTime.GetTime(state);
            }
            catch
            {
                await ReplyAsync("sigh... they're having another server issue");
                return;
                // make it send you a message
            }

            if (response == null)
            {
                await ReplyAsync("for some reason the response is null 🤔 \ndid you type the correct state name?");
                return;
            }

            await ReplyAsync(embed: new EmbedBuilder()
            {
                Title = "prayer times",
                Color = Color.Green,
                Fields = response
            }.Build()); 
        }

        [Command("states")]
        public async Task GetAvailableStates()
        {
            await Context.Channel.TriggerTypingAsync();

            var states = _prayerTime.GetStates();

            await ReplyAsync(embed: new EmbedBuilder()
            {
                Title = "Available States",
                Color = Color.Green,
                Fields = states
            }.Build());
        }
    }
}
