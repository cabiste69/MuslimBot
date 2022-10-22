using Discord;
using MuslimBot.Models;
using System.Text.Json;
using RestSharp;
using System.Reflection;

namespace MuslimBot.Services
{
    public class Prayer : IPrayer
    {
        private readonly RestClient _client;
        private readonly List<StateModel>? _states;
        public Prayer()
        {
            _client = new RestClient("https://www.meteo.tn/horaire_gouvernorat");

            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "MuslimBot.Resources.states.json";
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            {
                _states = JsonSerializer.Deserialize<List<StateModel>>(stream);
            }
        }

        public async Task<List<EmbedFieldBuilder>> GetTime(string state)
        {
            var (stateId, delegation) = FindIds(state);

            var request = new RestRequest(DateTime.Now.ToString("yyyy-MM-dd") + $"/{stateId}/{delegation}");
            var response = await _client.GetAsync<PrayerTimeModel>(request);
            if (response.data is null)
            {
                return null;
            }

            List<EmbedFieldBuilder> time = new(5);

            time.Add(new EmbedFieldBuilder() { Name = "Sobh", Value = response.data.sobh });
            time.Add(new EmbedFieldBuilder() { Name = "dhohr", Value = response.data.dhohr });
            time.Add(new EmbedFieldBuilder() { Name = "aser", Value = response.data.aser });
            time.Add(new EmbedFieldBuilder() { Name = "magreb", Value = response.data.magreb });
            time.Add(new EmbedFieldBuilder() { Name = "isha", Value = response.data.isha });        

            return time;
        }

        private (int stateId, int delegationId) FindIds(string state)
        {
            // default to the capital
            int stateId = 0; 
            int delegationId = 0;
            for (int i = 0; i < 24; i++)
            {
                if (_states[i].NameEn.ToUpper() == state.ToUpper())
                {
                    stateId = _states[i].Id;
                    delegationId = _states[i].Delegations.Find(x => x.NameEn == _states[i].NameEn).Id;
                    break;
                }
            }
            return (stateId, delegationId);
        }
    }
}
