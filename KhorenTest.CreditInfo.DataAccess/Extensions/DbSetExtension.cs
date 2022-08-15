using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace KhorenTest.CreditInfo.DataAccess.Extensions
{
    public static class DbSetExtension
    {
        public static IQueryable<T> IncludeMany<T>(this DbSet<T> set, params Expression<Func<T, object>>[] includes)
            where T : class
        {
            var result = set.AsQueryable();
            if (includes != null)
            {
                foreach (var expression in includes)
                {
                    result = result.Include(expression);
                }
            }
            return result;
        }
    }
}
