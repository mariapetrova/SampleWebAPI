using Domain.Entities;
using Nomenclatures.Application.IntegrationTests.Builders.Subareas.Queries;
using Nomenclatures.Application.IntegrationTests.Fixtures;
using Nomenclatures.Application.IntegrationTests.Builders.Subareas;
using FluentAssertions;

namespace Nomenclatures.Application.IntegrationTests.Subareas.Queries.SearchSubareas;

[Collection(HostBuilderCollectionFixture.Name)]
public class SearchSubareasQueryTests
{
    private readonly HostBuilderFixture _fixture;

    public SearchSubareasQueryTests(HostBuilderFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task SearchSpecialtiesQueryHandler_Should_ReturnListOfSpecialtiesResponseAsync()
    {
        // Arrange
        var subareaOne = new SubareaBuilder().WithId("2324").Build();
        var subareas = new List<Subarea>()
        {
            new SubareaBuilder().WithId("23245").Build(),
            new SubareaBuilder().WithId("23246").Build(),
            subareaOne
        };

        await _fixture.AddRangeAsync(subareas);

        var command = new SearchSubareaQueryBuilder().WithPINCode("2324").Build();

        // Act
        var response = await _fixture.SendAsync(command);

        // Assert
        response.Should().NotBeNull();
        response!.Should().HaveCount(subareas.Count);

        var specialtyResponseOne = subareas.SingleOrDefault(s => s.PINCode == "2324");
        specialtyResponseOne.Should().NotBeNull();
        specialtyResponseOne!.PINCode.Should().Be(subareaOne.PINCode);
        specialtyResponseOne!.Name.Should().Be(subareaOne.Name);
    }
}