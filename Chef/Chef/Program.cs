using Chef.Abstractions;
using Chef.Core;
using Chef.Core.Models;
using Chef.Core.Models.Spices.Salts;
using Chef.Core.Models.Vegetables.Onions;
using Chef.Core.Models.Vegetables.Tomatos;
using Chef.Core.Specifications;
using System;
using System.Linq;

namespace Chef
{
    class Program
    {
        static void Main(string[] args)
        {
            ISalad salad = new Salad("Буржуйский");

            #region Models
            var manufacturer = new Manufacturer("Puhalski and co.", "Belarus", "Kabyaka street 27");

            var cherry = new Ingredient(new Cherry("Черри Блосэм", new DateTime(2021, 11, 25), TomatoShape.HeartShaped, manufacturer), 200, 1);
            var bullHeart = new Ingredient(new BullHeart("Бычье сердце белое", new DateTime(2021, 11, 26), TomatoShape.Oval, manufacturer), 100, 2);
            var leeks = new Ingredient(new Leeks("Лук-порей", new DateTime(2021, 11, 24), OnionSort.SemiSharp, manufacturer), 20, 1);
            var sturon = new Ingredient(new Sturon("Стурон", new DateTime(2021, 11, 23), OnionSort.Sweet, manufacturer), 10, 2);

            var himalayanSalt = new Ingredient(new HimalayanPinkSalt("Соль гималайская розовая", new DateTime(2021, 10, 12), manufacturer), 2, 1);
            var indianSalt = new Ingredient(new IndianBlackSalt("Соль индийская черная", new DateTime(2021, 10, 11), manufacturer), 2, 1);
            #endregion

            salad.Add(cherry);
            salad.Add(bullHeart);
            salad.Add(leeks);
            salad.Add(sturon);
            salad.Add(himalayanSalt);
            salad.Add(indianSalt);

            Console.WriteLine($"Каллорийность салата: {salad.TotalCaloric}");

            salad.Remove(leeks);

            Console.WriteLine($"Каллорийность салата (без лука-порея): {salad.TotalCaloric}");

            Console.WriteLine("Сортировка овощей салата по дате созревания(по возрастанию):");

            foreach (var item in salad.SortBy(x => x.RipeningDate))
            {
                Console.WriteLine($"{item.Name} - {item.RipeningDate.ToShortDateString()}");
            }

            Console.WriteLine("Сортировка овощей салата по каллорийности(по убыванию):");

            foreach (var item in salad.SortByDescending(x => x.CaloriePerHundred))
            {
                Console.WriteLine($"{item.Name} - {item.CaloriePerHundred}kcal");
            }

            Console.WriteLine($"Каллорийность овощей в диапазоне от 10 до 100 :");

            var vegetableByCaloric = salad.FindBy(x => x.CaloriePerHundred >= 10 && x.CaloriePerHundred <= 100);

            if (!vegetableByCaloric.Any())
            {
                throw new ArgumentException();
            }
            else
            {
                foreach (var item in vegetableByCaloric)
                {
                    Console.WriteLine($"{item.Name} - {item.CaloriePerHundred}kcal");
                }
            }

            Console.ReadLine();
        }
    }
}
