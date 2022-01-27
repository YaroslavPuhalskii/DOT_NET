using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace WebSales.Models.Sale
{
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
}