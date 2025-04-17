using Demo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Data.configs.EmployeeConfigs
{
    public class EmployeeConfigurations :BaseEntityConfigurations<Employee> ,IEntityTypeConfiguration<Employee>
    {
        public new void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(e => e.Name).IsRequired().HasColumnType("varchar(50)");
            builder.Property(e => e.Address).IsRequired().HasColumnType("varchar(150)");
            builder.Property(e => e.Salary).HasColumnType("decimal(18,2)");
            builder.Property(e => e.EmployeeGender).HasConversion(
                gender => gender.ToString(),  //value will be string in database
                gender => (EmployeeGender)Enum.Parse(typeof(EmployeeGender), gender)  //casting string data from database to enum 
                );

            builder.Property(e => e.EmployeeType).HasConversion(
               type => type.ToString(),  //value will be string in database
               type => (EmployeeType)Enum.Parse(typeof(EmployeeType), type)  //casting string data from database to enum 
               );

            //builder.Property(d => d.CreatedOn).HasDefaultValueSql("GetDate()");//stay the same after insert [can be set manually] [creation date]
            //builder.Property(d => d.LastModifiedOn).HasComputedColumnSql("GetDate()");//recalculated automatically [not manually set] [Updated date]

            base.Configure(builder);
        }
    }
}
