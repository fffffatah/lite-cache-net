namespace LiteCache.Net;

public class Configs
{
    public bool AuthenticationEnabled { get; set; }
    public string? Username { get; set; }
    public string? Password { get; set; }
    public int SessionTimeout { get; set; }
}