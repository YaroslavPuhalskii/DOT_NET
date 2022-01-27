using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace WebSales.Models
{
    public class SaleFilter
    {
        public string Client { get; set; }

        public string Manager { get; set; }

        public string Product { get; set; }

        public int Sum { get; set; }
    }

    public class SaleIndexView
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Display(Name = "Manager name")]
        public string Manager { get; set; }

        [Display(Name = "Client name")]
        public string Client { get; set; }

        [Display(Name = "Product name")]
        public string Product { get; set; }

        [Display(Name = "Sum")]
        public int Sum { get; set; }

        [Display(Name = "Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Date { get; set; }
    }

    public class SaleCreateView
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Display(Name = "Client name")]
        [Required(ErrorMessage = "Select client!")]
        public int ClientId { get; set; }

        [Display(Name = "Manager name")]
        [Required(ErrorMessage = "Select manager!")]
        public int ManagerId { get; set; }

        [Display(Name = "Product name")]
        [Required(ErrorMessage = "Select product!")]
        public int ProductId { get; set; }

        [Display(Name = "Sum")]
        [Required(ErrorMessage = "Enter sum!")]
        [Range(0, int.MaxValue, ErrorMessage = "Enter correct data!")]
        public int Sum { get; set; }

        [Display(Name = "Date")]
        [Required(ErrorMessage = "Enter Date!")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime Date { get; set; }
    }

    public class SaleEditView
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Display(Name = "Manager")]
        [Required(ErrorMessage = "Select manager!")]
        public int ManagerId { get; set; }

        [Display(Name = "Client")]
        [Required(ErrorMessage = "Select client!")]
        public int ClientId { get; set; }

        [Display(Name = "Product")]
        [Required(ErrorMessage = "Select product!")]
        public int ProductId { get; set; }

        [Display(Name = "Sum")]
        [Required(ErrorMessage = "Enter sum!")]
        [Range(1, int.MaxValue, ErrorMessage = "Enter correct data!")]
        public int Sum { get; set; }

        [Display(Name = "Date")]
        [Required(ErrorMessage = "Enter Date!")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime Date { get; set; }
    }
}