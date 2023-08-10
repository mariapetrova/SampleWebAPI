using FluentAssertions;
using Leads.Application.IntegrationTests.Builders;
using Leads.Application.IntegrationTests.Builders.Queries;
using Leads.Application.IntegrationTests.Fixtures;

namespace Leads.Application.IntegrationTests.Leads.Queries;

[Collection(HostBuilderCollectionFixture.Name)]
public class GetLeadQueryTests
{
    private readonly HostBuilderFixture _fixture;

    public GetLeadQueryTests(HostBuilderFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task GetLeadQueryHandler_Authorised_ReturnLeadResponseAsync()
    {
        // Arrange
        var lead = await _fixture.AddAsync(new LeadBuilder().WithPINCode("234").Build());

        var query = new GetLeadQueryBuilder().WithId(lead.Id).Build();

        // Act
        var response = await _fixture.SendAsync(query);

        // Assert
        response.Should().NotBeNull();
        response!.Id.Should().Be(lead.Id);
        response!.Name.Should().Be(lead.Name);
    }
}