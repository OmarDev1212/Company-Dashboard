using Demo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Data.configs.DepartmentConfigs
{
    public class DeparmentConfigurations :BaseEntityConfigurations<Department>, IEntityTypeConfiguration<Department>
    {
        // id of department start with 10 and increase by 10 
        // name nvarchar(20)
        // code nvarchar(20)
        //default value for createdOn and LastModifiedOn 
        
        public new void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.Property(d => d.Id).UseIdentityColumn(10, 10);

            builder.Property(d => d.Name).HasColumnType("nvarchar(20)");

            builder.Property(d => d.Code).HasColumnType("nvarchar(20)");
            //builder.Property(d => d.CreatedOn).HasDefaultValueSql("GetDate()");//stay the same after insert [can be set manually] [creation date]
            //builder.Property(d => d.LastModifiedOn).HasComputedColumnSql("GetDate()");//recalculated automatically [not manually set] [Updated date]

            base.Configure(builder);
        }
    }
}
