using Leads.Application.IntegrationTests.Builders.Commands;
using Leads.Application.LeadsHandlers.Commands.CreateLead;
using FluentAssertions;
using Domain.Entities;
using LinqKit;
using Leads.Application.IntegrationTests.Fixtures;
using Nomenclatures.Application.IntegrationTests.Builders.Subareas;
using Microsoft.EntityFrameworkCore;
using Application.Common.Exceptions;

namespace Leads.Application.IntegrationTests.Leads.Commands;

[Collection(HostBuilderCollectionFixture.Name)]
public class CreateLeadCommandTests
{
    private readonly HostBuilderFixture _fixture;

    public CreateLeadCommandTests(HostBuilderFixture fixture)
    {
        _fixture = fixture;
    }
    
    [Fact]
    public async Task ValidationBehaviour_InvalidEntity_ShoudThrowValidationExceptionAsync()
    {
        // Arrange
        var command = new CreateLeadCommand() { PINCode ="234" };

        // Act
        var action = () => _fixture.SendAsync(command);

        // Assert
        await action.Should().ThrowAsync<ValidationException>();
    }

    [Fact]
    public async Task CreateLeadCommandHandler_ValidEntity_CreatesLeadAsync()
    {
        // Arrange
        var subarea = new SubareaBuilder().WithId("234").Build();

        await _fixture.AddAsync(subarea);

        var command = new CreateLeadCommandBuilder().WithPINCode(subarea.PINCode).Build();

        // Act
        await _fixture.SendAsync(command);

        // Assert
        var predicate = PredicateBuilder.New<Lead>(false);
        predicate.And(r => r.Name == command.Name);

        var entity = await _fixture.GetAsync(predicate, CancellationToken.None);

        entity.Should().NotBeNull();
        entity!.Name.Should().Be(command.Name);
        entity.PINCode.Should().Be(command.PINCode);
    }
}