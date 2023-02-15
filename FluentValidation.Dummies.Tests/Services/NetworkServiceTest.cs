using System.Net.NetworkInformation;
using FakeItEasy;
using FluentAssertions;
using FluentAssertions.Extensions;
using FluentValidation.Dummies.Services;
using FluentValidation.Dummies.Services.DNS;

namespace FluentValidation.Dummies.Tests.Services;

public class NetworkServiceTest
{
    private readonly NetworkService _pingService;
    private readonly IDNS _dns;

    public NetworkServiceTest()
    {
        //Dependencies
        _dns = A.Fake<IDNS>();
        
        // SUT
        _pingService = new NetworkService(_dns );
    }

    [Fact]
    public void NetworkService_SendPing_ReturnString()
    {
        // Arrange
        A.CallTo(() => _dns.SendDNS())
            .Returns(true);
        // var pingService = new NetworkService(); // this is SUT (System Under Test) We could move to repository
        
        // Act
        var result = _pingService.SendPing();

        // Assert
        result.Should().NotBeNullOrWhiteSpace();
        result.Should().Be("Success: Ping Send!");
        result.Should().Contain("Success", Exactly.Once());
    }

    [Theory]
    [InlineData(1, 1, 2)]
    [InlineData(2, 2, 4)]
    public void NetworkService_PingTimeOut_ReturnInt(int a, int b, int expected)
    {
        // Arrange
        // var pingService = new NetworkService();

        // Act
        var result = _pingService.PingTimeOut(a, b);

        // Assert
        result.Should().Be(expected);
        result.Should().BeGreaterThanOrEqualTo(2);
        result.Should().NotBeInRange(-10000, 0);
    }
    
    [Fact]
    public void NetworkService_LastPingDate_ReturnDate()
    {
        // Arrange
        // var pingService = new NetworkService(); // this is SUT (System Under Test) We could move to repository
        
        // Act
        var result = _pingService.LastPingDate();

        // Assert
        result.Should().BeAfter(1.January(2010));
        result.Should().BeBefore(1.January(2030));
    }

    [Fact]
    public void NetworkService_GetPingOptions_ReturnObject()
    {
        // Arrange
        var expected = new PingOptions
        {
            DontFragment = true,
            Ttl = 1
        };

        // Act
        var result = _pingService.GetPingOptions();

        // Assert WARNING: "Be" careful
        result.Should().BeOfType<PingOptions>();
        result.Should().BeEquivalentTo(expected);
        result.Ttl.Should().Be(1);
    }

    [Fact]
    public void NetworkService_GetPingOptions_ReturnIEnumerableObject()
    {
        // Arrange
        var expected = new PingOptions
        {
            DontFragment = true,
            Ttl = 1
        };
        
        // Act
        var result = _pingService.MostRecentPings();

        // Assert
        result.Should().BeOfType<List<PingOptions>>();
        result.Should().ContainEquivalentOf(expected);
        result.Should().Contain(x => x.DontFragment);
    }
}