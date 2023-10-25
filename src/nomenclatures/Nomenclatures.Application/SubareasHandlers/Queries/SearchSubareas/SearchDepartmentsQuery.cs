namespace Nomenclatures.Application.SubareasHandlers.Queries.SearchSubareas;
public record SearchDepartmentsQuery() : IRequest<List<Department>>
{
    public int? Id { get; set; }
}