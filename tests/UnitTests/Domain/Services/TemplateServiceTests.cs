namespace WordMix.UnitTests.Domain.Services;

using FluentAssertions;
using Microsoft.Extensions.Logging.Abstractions;
using WordMix.Domain.Services;

public class TemplateServiceTests
{
    private readonly TemplateService _service = new(NullLogger<TemplateService>.Instance);

    [Fact]
    public void GetId_AnyNumber_ReturnsSameValue()
    {
        // Arrange
        var id = 1;

        // Act
        var getResult = _service.GetId(id);

        // Assert
        getResult.Should().Be(1);
    }
}