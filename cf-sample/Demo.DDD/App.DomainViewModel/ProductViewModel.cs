using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Mvc;
using App.Domain;

namespace App.DomainViewModel
{
    public class ProductViewModel : BaseViewModel
    {
        
        [DisplayName("Product Name")]
        [Required(ErrorMessage = "Product Name is required.")]
        [MaxLength(200)]
        public string Name { get; set; }
        [DisplayName("Product Price")]
        [Required(ErrorMessage = "Product Price is required.")]
        public float Price { get; set; }
        public float Quantity { get; set; }
        public float Total { get; set; }
        [Required(ErrorMessage = "Select one category.")]
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        public List<SelectListItem> ddlCategories { get; set; }
    }
}
