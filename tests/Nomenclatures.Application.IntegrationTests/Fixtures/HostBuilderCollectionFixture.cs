namespace Nomenclatures.Application.IntegrationTests.Fixtures;

[CollectionDefinition(Name)]
public class HostBuilderCollectionFixture : ICollectionFixture<HostBuilderFixture>
{
    public const string Name = "Nomenclatures collection";
}