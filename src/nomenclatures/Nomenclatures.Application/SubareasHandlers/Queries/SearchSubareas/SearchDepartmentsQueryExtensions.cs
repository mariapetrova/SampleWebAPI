namespace Nomenclatures.Application.SubareasHandlers.Queries.SearchSubareas;
public static class SearchDepartmentsQueryExtensions
{
    public static Expression<Func<Department, bool>> Filter(
      this SearchDepartmentsQuery searchCriteria)
    {
        var predicate = PredicateBuilder.New<Department>(true);

        if (searchCriteria.Id != null)
        {
            predicate.And(r => r.Id == searchCriteria.Id);
        }

        return predicate;
    }
}