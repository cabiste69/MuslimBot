using Discord;
using Discord.WebSocket;

namespace MuslimBot
{
    class RandomStuff
    {
        async void SendEmbed()
        {
            DiscordSocketClient _client = new DiscordSocketClient(); 
            ulong id = 123456789012345678; // channel id
            var chnl = _client.GetChannel(id) as IMessageChannel; // get the channel
            await chnl.SendMessageAsync("Announcement!", embed: new EmbedBuilder() // build an embed to send
            {
                Title = "new embed",
                Description = "",
            }.Build()); 
        }
    }
}
