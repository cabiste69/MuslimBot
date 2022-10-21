using Discord;

namespace MuslimBot.Services;

public interface IPrayer
{
    Task<List<EmbedFieldBuilder>> GetTime(string state);
}