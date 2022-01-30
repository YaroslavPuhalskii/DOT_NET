using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace WebSales.Models.Product
{
    public class ProductIndexView
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Category")]
        public string Category { get; set; }
    }
}