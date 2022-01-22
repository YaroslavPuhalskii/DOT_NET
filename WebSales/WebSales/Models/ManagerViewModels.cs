using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace WebSales.Models
{
    public class ManagerIndexView
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Менеджер")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Длина строки от 2 до 50")]
        public string Name { get; set; }
    }

    public class ManagerCreateView
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Менджер")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Длина строки от 2 до 50")]
        public string Name { get; set; }
    }

    public class ManagerEditView
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Длина строки от 2 до 50")]
        [Display(Name = "Имя менеджера")]
        public string Name { get; set; }
    }
}