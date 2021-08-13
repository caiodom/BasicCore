using Core.DomainObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Extensions
{
    public static class DbContextExtensions
    {

        public static void DetachLocal<T>(this DbContext context, T entity, Guid entryId) where T:BaseEntity
        {
            var local = context.Set<T>()
                .Local
                .FirstOrDefault(entry => entry.Id.Equals(entryId));

            if (local != null)
                context.Entry(local).State = EntityState.Detached;

            context.Entry(entity).State = EntityState.Modified;
        }
    }
}
