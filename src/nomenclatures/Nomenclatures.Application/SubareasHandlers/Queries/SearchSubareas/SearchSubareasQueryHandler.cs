using Application.Common.Interfaces;

namespace Nomenclatures.Application.SubareasHandlers.Queries.SearchSubareas;
public class SearchSubareasQueryHandler : IRequestHandler<SearchSubareasQuery, List<Subarea>>
{
    private readonly IApplicationDbContext _context;

    public SearchSubareasQueryHandler(
        IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Subarea>> Handle(
        SearchSubareasQuery request,
        CancellationToken cancellationToken)
    {
        var subareas = await _context.Subareas
            .Where(request.Filter())
            .ToListAsync(cancellationToken);

        return subareas;
    }
}