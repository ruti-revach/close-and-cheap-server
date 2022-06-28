using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace close_and_cheap.Data.Entities
{
    public class User
    {
        public User(){ }

        public User(int id, string Name,string email, string password, string address, int roleId)
        {
            this.Id = id;
            this.Name = Name;
            this.Password = password;
            Address = address;
            RoleId = roleId;
            Email = email;
        }        public User(int id, string Name,string email, string password, int roleId)
        {
            this.Id = id;
            this.Name = Name;
            this.Password = password;
            RoleId = roleId;
            Email = email;
        }
 
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(150)]
        public string Name { get; set; }
        [Required]
        [StringLength(100)]
        public string Email{ get; set; }

        [Required]
        public string Password { get; set; }
        public string Address { get; set; }

        public ICollection<ClaimData> Claims { get; set; }


        public int RoleId { get; set; }
        public virtual Role Role { get; set; }
    }
}
