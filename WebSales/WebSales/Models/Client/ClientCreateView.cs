using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace WebSales.Models.Client
{
    public class ClientCreateView
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Enter Name!")]
        [Display(Name = "Name")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Length from 2 to 50!")]
        public string Name { get; set; }
    }
}