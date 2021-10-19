using RestAspNet5DockerAzure.Model.Base;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestAspNet5DockerAzure.Model
{
    [Table("UsersRoles")]
    public class UserRole : BaseEntity
    {
        [ForeignKey("Users")]
        public long UsersId { get; set; }

        [ForeignKey("Roles")]
        public long RolesId { get; set; }

    }
}
