using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace WebSales.Models.Manager
{
    public class ManagerIndexView
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }
    }
}