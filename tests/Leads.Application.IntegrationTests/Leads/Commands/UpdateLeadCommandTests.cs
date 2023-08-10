using Application.Common.Exceptions;
using Domain.Entities;
using FluentAssertions;
using Leads.Application.IntegrationTests.Builders;
using Leads.Application.IntegrationTests.Builders.Commands;
using Leads.Application.IntegrationTests.Fixtures;
using Leads.Application.LeadsHandlers.Commands.UpdateLead;
using LinqKit;
using Nomenclatures.Application.IntegrationTests.Builders.Subareas;

namespace Leads.Application.IntegrationTests.Leads.Commands;

[Collection(HostBuilderCollectionFixture.Name)]
public class UpdateLeadCommandTests
{
    private readonly HostBuilderFixture _fixture;

    public UpdateLeadCommandTests(HostBuilderFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task ValidationBehaviour_InvalidEntity_ShoudThrowValidationExceptionAsync()
    {
        // Arrange
        var command = new UpdateLeadCommand();

        // Act
        var action = () => _fixture.SendAsync(command);

        // Assert
        await action.Should().ThrowAsync<ValidationException>();
    }

    [Fact]
    public async Task UpdateLeadCommandHandler_ValidEntity_UpdatesLeadAsync()
    {
        // Arrange
        var leadId = 2;
        await _fixture.AddAsync(new LeadBuilder().WithId(leadId).WithPINCode("2347").Build());

        var subarea = new SubareaBuilder().WithId("2347").Build();
       
        await _fixture.AddAsync(subarea);

        var updateCommand = new UpdateLeadCommandBuilder().WithId(leadId).WithPINCode(subarea.PINCode).Build();

        // Act
        await _fixture.SendAsync(updateCommand);

        // Assert
        var predicate = PredicateBuilder.New<Lead>(false);
        predicate.And(r => r.Id == leadId);

        var entity = await _fixture.GetAsync(predicate, CancellationToken.None);

        entity.Should().NotBeNull();
    }
}