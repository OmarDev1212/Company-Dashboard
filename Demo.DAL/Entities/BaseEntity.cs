using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Entities
{
    public class BaseEntity
    {
        public int Id { get; set; } //PK
        public int CreatedBy  { get; set; }//userId who created record
        public int LastModifiedBy { get; set; } //user who modified record
        public DateTime CreatedOn { get; set; }//Time Of Create 
        public DateTime? LastModifiedOn { get; set; } //Automatically Calculated => Time of modification 
        public bool IsDeleted { get; set; }//soft Delete
    }
}
