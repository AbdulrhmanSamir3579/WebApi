using System.Linq.Expressions;

namespace Domain.Interfaces;

public interface ISpecifications<T>
{
    Expression<Func<T, bool>>? Criteria { get; }
    List<Expression<Func<T, object>>> Includes { get; }
}