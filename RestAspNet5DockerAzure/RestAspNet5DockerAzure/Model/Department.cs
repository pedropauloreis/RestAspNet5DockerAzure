using RestAspNet5DockerAzure.Model.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestAspNet5DockerAzure.Model
{
    [Table("departments")]
    public class Department : BaseEntity
    {
        [Column("name")]
        public string Name { get; set; }

        
    }
}
