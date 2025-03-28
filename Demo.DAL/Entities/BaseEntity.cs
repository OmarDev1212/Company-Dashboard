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
        public string CreatedBy  { get; set; }//userId
        public string LastModifiedBy { get; set; } //Time of modification
        public DateTime CreatedOn { get; set; }//Time Of Create 
        public DateTime? LastModifiedOn { get; set; } //Automatically Calculated 
        public bool IsDeleted { get; set; }//soft Delete
    }
}
