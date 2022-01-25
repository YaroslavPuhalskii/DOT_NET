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

        [Display(Name = "Продавец")]
        public string Manager { get; set; }

        [Display(Name = "Клиeнт")]
        public string Client { get; set; }

        [Display(Name = "Название продукта")]
        public string Product { get; set; }

        [Display(Name = "Стоимость")]
        public int Sum { get; set; }

        [Display(Name = "Дата продажи")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Date { get; set; }
    }

    public class SaleCreateView
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Display(Name = "Клиент")]
        [Required(ErrorMessage = "Выберите клиента")]
        public int ClientId { get; set; }

        [Display(Name = "Менеджер")]
        [Required(ErrorMessage = "Выберите менеджера")]
        public int ManagerId { get; set; }

        [Display(Name = "Продукт")]
        [Required(ErrorMessage = "Выберите продукт")]
        public int ProductId { get; set; }

        [Display(Name = "Стоимость")]
        [Required(ErrorMessage = "Введите стоимость")]
        [Range(0, int.MaxValue, ErrorMessage = "Введите корректные данные")]
        public int Sum { get; set; }

        [Display(Name = "Дата продажи")]
        [Required(ErrorMessage = "Введите дату продажи")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime Date { get; set; }
    }

    public class SaleEditView
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Display(Name = "Менеджер")]
        [Required(ErrorMessage = "Выберите менеджера")]
        public int ManagerId { get; set; }

        [Display(Name = "Клиент")]
        [Required(ErrorMessage = "Выберите клиента")]
        public int ClientId { get; set; }

        [Display(Name = "Продукт")]
        [Required(ErrorMessage = "Выберите продукт")]
        public int ProductId { get; set; }

        [Display(Name = "Стоимость")]
        [Required(ErrorMessage = "Введите стоимость")]
        [Range(1, int.MaxValue, ErrorMessage = "Введите корректные данные")]
        public int Sum { get; set; }

        [Display(Name = "Дата продажи")]
        [Required(ErrorMessage = "Введите дату продажи")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime Date { get; set; }
    }
}