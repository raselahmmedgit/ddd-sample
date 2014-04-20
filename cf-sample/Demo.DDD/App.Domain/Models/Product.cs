using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Domain
{
    public class Product : BaseModel
    {

        [DisplayName("Name")]
        [Required(ErrorMessage = "Name is required.")]
        [MaxLength(200)]
        public string Name { get; set; }

        [DisplayName("Product Price")]
        [Required(ErrorMessage = "Price is required.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Select one category.")]
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

    }
}
