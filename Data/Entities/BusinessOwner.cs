using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace close_and_cheap.Data.Entities
{
    public class BusinessOwner
    {
        public BusinessOwner()
        {

        }
        public BusinessOwner(int id, string name, string address,string phone, int categoryId)
        {
            this.Id = id;
            this.Name = name;
            this.Phone = phone;
            this.Address = address;
            this.CategoryId = categoryId;
        }
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(150)]
        public string Name { get; set; }
        [Required]
        [StringLength(150)]
        public string Address { get; set; }
        public string Phone { get; set; }

        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
}
