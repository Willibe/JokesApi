using JokesApi.DataContext;
using JokesApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace JokesApi.Helpers
{
    public static class JokeFilter
    {
        public static Expression<Func<TEntityModel, bool>> FilterCondition<TIdentifierType, TEntityModel>(TIdentifierType id, string propertyName)
        {
            var param = Expression.Parameter(typeof(TEntityModel), "model");
            var exp = Expression.Lambda<Func<TEntityModel, bool>>(
                Expression.Equal(
                    Expression.Property(param, propertyName),
                    Expression.Constant(id)
                    ),
                param);
            return exp;
        }



        public static IQueryable<TEntityModel> Filter<TIdentifierType, TEntityModel>(
            TIdentifierType identifier,
            string propertyName,
            DbSet<TEntityModel> context,
            IQueryable<TEntityModel>? queryToAttachTo = null)
            where TEntityModel : class
        {
            IQueryable<TEntityModel> jokes;
            var expression = FilterCondition<TIdentifierType, TEntityModel>(identifier, propertyName);

            if (queryToAttachTo != null)
            {
                jokes = queryToAttachTo.Where(expression);
                return jokes;
            }
            jokes = context.Where(expression);
            return jokes;
        }
    }

}

