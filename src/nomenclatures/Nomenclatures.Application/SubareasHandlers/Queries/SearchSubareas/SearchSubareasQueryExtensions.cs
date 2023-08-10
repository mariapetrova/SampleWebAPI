namespace Nomenclatures.Application.SubareasHandlers.Queries.SearchSubareas;
public static class SearchSubareasQueryExtensions
{
    public static Expression<Func<Subarea, bool>> Filter(
      this SearchSubareasQuery searchCriteria)
    {
        var predicate = PredicateBuilder.New<Subarea>(true);

        if (!string.IsNullOrWhiteSpace(searchCriteria.PINCode))
        {
            predicate.And(r => r.PINCode.Contains(searchCriteria.PINCode));
        }

        return predicate;
    }
}