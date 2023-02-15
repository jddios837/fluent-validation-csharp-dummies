using System.Net.NetworkInformation;
using FluentValidation.Dummies.Services.DNS;

namespace FluentValidation.Dummies.Services;

public class NetworkService
{
    private readonly IDNS _dns;

    public NetworkService(IDNS dns)
    {
        _dns = dns;
    }
    
    public string SendPing()
    {
        var dnsSuccess = _dns.SendDNS();
        return dnsSuccess ? "Success: Ping Send!" : "Failed: Ping not Sent!";
    }

    public int PingTimeOut(int a, int b)
    {
        return a + b;
    }

    public DateTime LastPingDate()
    {
        return DateTime.Now;
    }

    public PingOptions GetPingOptions()
    {
        return new PingOptions
        {
            DontFragment = true,
            Ttl = 1
        };
    }

    public IEnumerable<PingOptions> MostRecentPings()
    {
        IEnumerable<PingOptions> pingOptions = new List<PingOptions>()
        {
            new()
            {
                DontFragment = true,
                Ttl = 1
            },
            new()
            {
                DontFragment = true,
                Ttl = 1
            },
            new()
            {
                DontFragment = true,
                Ttl = 1
            }
        };

        return pingOptions;
    }
}