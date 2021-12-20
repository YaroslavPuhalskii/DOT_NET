using ATC.Abstractions;
using ATC.Abstractions.ATC;
using ATC.BillingSystem;
using ATC.BillingSystem.Reports;
using ATC.BillingSystem.TariffPlan;
using ATC.Models;
using ATC.Models.ATC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ATC
{
    class Program
    {
        static void Main(string[] args)
        {
            var @operator = new Operator();
            var station = new Station(3);
            var billing = new Billing(new BusinessTariffPlan());

            @operator.RegistrationInBilling += billing.Registration;
            @operator.RegestrationInStation += station.Add;
            station.EndOfCall += billing.EndOfCall;
            station.GetClient += @operator.GetClient;

            IClient c1 = new Client("Mike", "Gomez");
            IClient c2 = new Client("Maxim", "Swift");
            IClient c3 = new Client("Petr", "Ivanov");

            var t1 = @operator.Registarion(c1, 1500);
            var t2 = @operator.Registarion(c2, 1200);
            var t3 = @operator.Registarion(c3, 100);

            t1.Call(t2.Number);
            t2.Answer();
            t1.Reject();
            Thread.Sleep(1000);
            t1.Call(t3.Number);
            t3.Answer();
            t1.Reject();
            Thread.Sleep(1000);
            t2.Call(t1.Number);
            t1.Answer();
            t1.Reject();

            var report = new CallReport();
            Console.WriteLine("Sort by price :");
            foreach (var item in report.SortByPrice(billing, c1))
            {
                Console.WriteLine($"{item.DateTime} - {item.receiver} - {item.Price}");
            }

            Console.WriteLine("Sort by date :");
            foreach (var item in report.SortByDate(billing, c1))
            {
                Console.WriteLine($"{item.DateTime} - {item.receiver} - {item.time}s.");
            }

            Console.WriteLine($"{c1.FirstName}'s call report :");
            foreach (var item in report.GetCallReport(billing, c1))
            {
                Console.WriteLine(item.ToString());
            }

            Console.ReadLine();
        }
    }
}
