using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace WebSales.Models
{
    public class ProductIndexView
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Продукт")]
        public string Name { get; set; }
    }

    public class ProductCreateView
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Продукт")]
        public string Name { get; set; }
    }

    public class ProductEditView
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Продукт")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Длина строки от 2 букв до 50!")]
        public string Name { get; set; }
    }
}