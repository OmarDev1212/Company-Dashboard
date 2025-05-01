using Demo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Data.configs
{
    public class BaseEntityConfigurations<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : BaseEntity
    {
        public void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.Property(d => d.CreatedOn).HasDefaultValueSql("GetDate()");//stay the same after insert [can be set manually] [creation date]
            builder.Property(d => d.LastModifiedOn).HasComputedColumnSql("GetDate()");//recalculated automatically [not manually set] [Updated date]
        }
    }
}
