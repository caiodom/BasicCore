
using Core.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Extensions
{
    public abstract class EntityTypeConfiguration<TEntity> : IAggregateRoot where TEntity : class
    {
        public abstract void Configure(EntityTypeBuilder<TEntity> entity);

    }
}
