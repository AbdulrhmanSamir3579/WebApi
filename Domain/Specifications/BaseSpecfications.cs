using System.Linq.Expressions;
using Domain.Interfaces;

namespace Domain.Specifications;

public class BaseSpecfications<T> : ISpecifications<T>
{
    public BaseSpecfications()
    {
    }

    public BaseSpecfications(Expression<Func<T, bool>> critiera)
    {
        Criteria = critiera;
    }

    public Expression<Func<T, bool>>? Criteria { get; set; }
    public List<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>>();
}