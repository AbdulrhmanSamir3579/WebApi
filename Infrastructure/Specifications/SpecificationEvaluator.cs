using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Specifications;

public static class SpecificationEvaluator<T> where T : BaseEntity
{
    public static IQueryable<T> GetQuery(IQueryable<T> inputQuery, ISpecifications<T> specifications)
    {
        var query = inputQuery;
        if (specifications.Criteria != null)
        {
            query = query.Where(specifications.Criteria);
        }

        query = specifications.Includes.Aggregate(query, (cur, next) => cur.Include(next));
        return query;
    }
}