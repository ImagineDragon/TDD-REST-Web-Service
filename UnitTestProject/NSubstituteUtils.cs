using Microsoft.EntityFrameworkCore;
using NSubstitute;
using System.Collections.Generic;
using System.Linq;

namespace DefaultControllerTests
{
    class NSubstituteUtils
    {
        public static DbSet<T> CreateMockDbSet<T>(IEnumerable<T> data = null)
            where T : class
        {
            var dbSet = Substitute.For<DbSet<T>, IQueryable<T>>();

            if (data != null)
            {
                var queryable = data.AsQueryable();

                ((IQueryable<T>)dbSet).Provider.Returns(new TestAsyncQueryProvider<T>(queryable.Provider));
                ((IQueryable<T>)dbSet).Expression.Returns(queryable.Expression);
                ((IQueryable<T>)dbSet).ElementType.Returns(queryable.ElementType);
                ((IQueryable<T>)dbSet).GetEnumerator().Returns(queryable.GetEnumerator());
            }

            return dbSet;
        }
    }
}
