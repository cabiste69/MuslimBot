using Discord;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
