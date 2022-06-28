using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace close_and_cheap.Data.Entities
{
    public class Category
    {
        public Category()
        {

        }
        public Category(int id, string name)
        {
            Id = id;
            Name = name;
        }
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
    }
}
