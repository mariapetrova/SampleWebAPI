namespace Nomenclatures.Application.SubareasHandlers.Queries.SearchSubareas;
public record SearchSubareasQuery() : IRequest<List<Subarea>>
{
    public string PINCode { get; set; }
}