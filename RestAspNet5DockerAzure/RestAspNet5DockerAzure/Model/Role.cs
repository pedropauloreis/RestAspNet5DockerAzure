using RestAspNet5DockerAzure.Model.Base;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestAspNet5DockerAzure.Model
{
    [Table("roles")]
    public class Role : BaseEntity
    {
        [Column("name")]
        public string Name { get; set; }

    }
}
