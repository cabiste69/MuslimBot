namespace MuslimBot.Models;


// link example:
// https://www.meteo.tn/horaire_gouvernorat/2022-10-16/361/634
public sealed class PrayerTimeModel
{
    public Data data { get; set; }
}

 public sealed class Data
{
    public string sobh { get; set; }
    public string dhohr { get; set; }
    public string aser { get; set; }
    public string magreb { get; set; }
    public string isha { get; set; }
}
