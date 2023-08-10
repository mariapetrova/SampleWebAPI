using Nomenclatures.Application.SubareasHandlers.Queries.SearchSubareas;

namespace Nomenclatures.Application.IntegrationTests.Builders.Subareas.Queries;

public class SearchSubareaQueryBuilder
{
    private readonly SearchSubareasQuery _command = new();

    public SearchSubareasQuery Build()
    {
        return _command;
    }

    public SearchSubareaQueryBuilder WithPINCode(string pinCode)
    {
        _command.PINCode = pinCode;
        return this;
    }
}