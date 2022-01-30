using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace WebSales.Models.Product
{
    public class ProductCreateView
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Enter Name!")]
        [Display(Name = "Name")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Length from 2 to 50!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Enter Category!")]
        [Display(Name = "Category")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Length from 2 to 50!")]
        public string Category { get; set; }
    }
}