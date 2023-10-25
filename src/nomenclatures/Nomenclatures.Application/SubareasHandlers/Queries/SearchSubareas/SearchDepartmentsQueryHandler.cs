using Application.Common.Interfaces;

namespace Nomenclatures.Application.SubareasHandlers.Queries.SearchSubareas;
public class SearchDepartmentsQueryHandler : IRequestHandler<SearchDepartmentsQuery, List<Department>>
{
    private readonly IApplicationDbContext _context;

    public SearchDepartmentsQueryHandler(
        IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Department>> Handle(
        SearchDepartmentsQuery request,
        CancellationToken cancellationToken)
    {
        var departments = await _context.Departments
            .Where(request.Filter())
            .ToListAsync(cancellationToken);

        return departments;
    }
}