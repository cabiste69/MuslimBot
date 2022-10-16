namespace MuslimBot.Models;


// link example:
// https://www.meteo.tn/horaire_gouvernorat/2022-10-16/361/634
public class PrayerTimeModel
{
    public Data data { get; set; }
}

public class Data
{
    public int id { get; set; }
    public Gouvernorat gouvernorat { get; set; }
    public Delegation delegation { get; set; }
    public string sobh { get; set; }
    public string dhohr { get; set; }
    public string aser { get; set; }
    public string magreb { get; set; }
    public string isha { get; set; }
}

public class Gouvernorat
{
    public string intituleAn { get; set; }
}

public class Delegation
{
    public string intituleAn { get; set; }
}
