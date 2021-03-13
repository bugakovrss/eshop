using System;
using System.Linq;
using System.Linq.Expressions;

namespace Eshop.ProductApi.Extensions
{
    public static class DbContextExtensions
    {
        /// <summary>
        /// Filter database data
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="filter"></param>
        /// <param name="filterApplyCondition">Condition applied condition</param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        public static IQueryable<T> ApplyFilter<T>(this IQueryable<T> query, Expression<Func<T, bool>> filter,
            Func<bool> filterApplyCondition, int skip, int take)
        {
            IQueryable<T> internalQuery = query;

            if (filterApplyCondition())
            {
                internalQuery = internalQuery.Where(filter);
            }

            return internalQuery.Skip(skip).Take(take);
        }
    }
}
