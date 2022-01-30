using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace WebSales.Models.Client
{
    public class ClientIndexView
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Name")]
        public int Age { get; set; }
    }
}