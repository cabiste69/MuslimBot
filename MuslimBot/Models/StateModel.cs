namespace MuslimBot.Models;

public sealed class StateModel
{
    public int Id { get; set; }
    public string NameAr { get; set; }
    public string NameEn { get; set; }
    public List<Delegation> Delegations { get; set; }
}

public sealed class Delegation
{
    public int Id { get; set; }
    public string NameAr { get; set; }
    public string NameEn { get; set; }
}
