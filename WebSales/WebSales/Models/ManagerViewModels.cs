using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace WebSales.Models
{
    public class ManagerIndexView
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }
    }

    public class ManagerCreateView
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Name")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Length from 2 to 50!")]
        public string Name { get; set; }
    }

    public class ManagerEditView
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Name")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Length from 2 to 50!")]
        public string Name { get; set; }
    }
}