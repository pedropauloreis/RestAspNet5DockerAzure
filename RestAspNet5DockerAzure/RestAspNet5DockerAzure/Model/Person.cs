using RestAspNet5DockerAzure.Model.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestAspNet5DockerAzure.Model
{
    [Table("persons")]
    public class Person : BaseEntity
    {
        
        [Column("first_name")]
        public string FirstName { get; set; }

        [Column("last_name")]
        public string LastName { get; set; }

        [Column("address")]
        public string Address { get; set; }

        [Column("gender")]
        public string Gender { get; set; }

        [ForeignKey("departmentid")]
        public long DepartmentId { get; set; }
        public Department Department { get; set; }


    }
}
