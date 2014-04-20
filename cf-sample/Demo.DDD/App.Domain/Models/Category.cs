using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace App.Domain
{
    public class Category : BaseModel
    {
        [Display(Name = "Name")]
        [Required(ErrorMessage = "Name is required")]
        public virtual string Name { get; set; }
    }
}
