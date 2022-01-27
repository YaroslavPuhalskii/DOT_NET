using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace WebSales.Models
{
    public class ClientIndexView
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Имя")]
        public string Name { get; set; }
    }

    public class ClientCreateView
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Имя")]
        public string Name { get; set; }
    }

    public class ClientEditView
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Имя")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Длина строки от 2 букв до 50!")]
        public string Name { get; set; }
    }
}