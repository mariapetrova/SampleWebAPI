using Leads.Application.LeadsHandlers.Queries;

namespace Leads.Application.IntegrationTests.Builders.Queries;
public class GetLeadQueryBuilder
{
    private readonly GetLeadQuery _query = new();

    public GetLeadQuery Build()
    {
        return _query;
    }

    public GetLeadQueryBuilder WithId(int id)
    {
        _query.Id = id;
        return this;
    }
}