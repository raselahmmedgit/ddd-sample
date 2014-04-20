using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using App.Domain;

namespace App.DomainViewModel
{
    public class CategoryViewModel : BaseViewModel
    {
        [Display(Name = "Name")]
        [Required(ErrorMessage = "Category Name is required")]
        public virtual string Name { get; set; }

        public virtual ICollection<ProductViewModel> ProductViewModels { get; set; }
    }
}
