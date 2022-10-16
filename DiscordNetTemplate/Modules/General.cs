using Discord;
using Discord.Commands;
using Discord.Rest;
using MuslimBot.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuslimBot.Modules
{
    public class General : ModuleBase<SocketCommandContext>
    {
        [Command("ping ss")]
        [Alias("p")]
        
        public async Task PingAsync()
        {
            await Context.Channel.TriggerTypingAsync();
            await Context.Channel.SendMessageAsync("Pong!");
        }

        [Command("PrayTime")]
        [Alias("PrayTime", "Pray Time", "PrayerTime", "Prayer Time", "time")]
        public async Task GetPrayerTime()
        {
            try
            {
                var client = new RestClient("https://www.meteo.tn/horaire_gouvernorat");
                var request = new RestRequest(DateTime.Now.ToString("yyyy-MM-dd") + "/361/634");
                var response = await client.GetAsync<PrayerTimeModel>(request);
                if (response.data is not null)
                {
                    var sobh   = new EmbedFieldBuilder() { Name = "Sobh", Value = response.data.sobh };
                    var dhohr  = new EmbedFieldBuilder() { Name = "dhohr", Value = response.data.dhohr };
                    var aser   = new EmbedFieldBuilder() { Name = "aser", Value = response.data.aser };
                    var magreb = new EmbedFieldBuilder() { Name = "magreb", Value = response.data.magreb };
                    var isha   = new EmbedFieldBuilder() { Name = "isha", Value = response.data.isha };

                    await ReplyAsync(embed: new EmbedBuilder()
                    {
                        Title = "prayer times",
                        Color = Color.Green,
                        Fields = new List<EmbedFieldBuilder> { sobh, dhohr, aser, magreb, isha }
                    }.Build()); ;
                }
                else
                {
                    await ReplyAsync("response is null :(");
                }

            }
            catch (Exception e)
            {
                await ReplyAsync(e.Message);
                // make it send you a message
            }

        }
    }
}
