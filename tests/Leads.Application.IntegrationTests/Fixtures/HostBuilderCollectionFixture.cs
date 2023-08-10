namespace Leads.Application.IntegrationTests.Fixtures;

[CollectionDefinition(Name)]
public class HostBuilderCollectionFixture : ICollectionFixture<HostBuilderFixture>
{
    public const string Name = "Lead collection";

    // This class has no code, and is never created. Its purpose is simply
    // to be the place to apply [CollectionDefinition] and all the
    // ICollectionFixture<> interfaces.
}
