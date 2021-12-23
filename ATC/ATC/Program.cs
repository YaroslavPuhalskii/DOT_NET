using ATC.Abstractions;
using ATC.Abstractions.ATC;
using ATC.BillingSystem;
using ATC.BillingSystem.Reports;
using ATC.BillingSystem.TariffPlan;
using ATC.Core;
using ATC.Core.Models;
using System;
using System.Threading;

namespace ATC
{
    class Program
    {
        static void Main(string[] args)
        {
            var @operator = new Operator();
            IStation station = new Station(3);
            var billing = new Billing(new BusinessTariffPlan());

            @operator.RegistrationInBilling += billing.Registration;
            @operator.RegistrationInStation += station.Add;
            station.EndOfCall += billing.EndOfCall;
            station.GetClient += @operator.GetClient;


            IClient c1 = new Client("Mike", "Gomez");
            IClient c2 = new Client("Maxim", "Swift");
            IClient c3 = new Client("Petr", "Swift");

            var t1 = @operator.Registarion(c1, 1500);
            var t2 = @operator.Registarion(c2, 1200);
            var t3 = @operator.Registarion(c3, 100);

            TestATC(t1, t2, t3);

            TestBillingSystem(billing, c1);

            @operator.RegistrationInBilling -= billing.Registration;
            @operator.RegistrationInStation -= station.Add;
            station.EndOfCall -= billing.EndOfCall;
            station.GetClient -= @operator.GetClient;

            Console.ReadLine();
        }

        private static void TestATC(ITerminal t1, ITerminal t2, ITerminal t3)
        {
            if (t1 == null || t2 == null || t3 == null)
            {
                throw new ArgumentNullException($"{nameof(t1)} or {nameof(t2)} or {nameof(t3)} is null!");
            }

            t1.Call(t2.Number);
            t2.Answer();
            t1.Reject();
            Thread.Sleep(1000);

            t1.Disconnect();
            t1.Call(t2.Number);
            t1.Connect();

            t1.Call(t3.Number);
            t3.Answer();
            t1.Reject();
            Thread.Sleep(1000);

            t2.Call(t1.Number);
            t1.Answer();
            t1.Reject();
        }

        private static void TestBillingSystem(Billing billing, IClient client)
        {
            if (billing == null || client == null)
            {
                throw new ArgumentNullException($"{nameof(billing)} or {nameof(client)} is null!");
            }

            var report = new CallReport();

            Console.WriteLine("Sort by price :");
            foreach (var item in report.SortByPrice(billing, client))
            {
                Console.WriteLine($"{item.DateTime} - {item.Receiver} - {item.Price}");
            }

            Console.WriteLine("Sort by date :");
            foreach (var item in report.SortByDate(billing, client))
            {
                Console.WriteLine($"{item.DateTime} - {item.Receiver} - {item.Time}s.");
            }

            Console.WriteLine("Sort by receiver:");
            foreach (var item in report.SortByReceiver(billing, client))
            {
                Console.WriteLine($"{item.Receiver}");
            }

            Console.WriteLine($"{client.FirstName}'s call report :");
            foreach (var item in report.GetCallReport(billing, client))
            {
                Console.WriteLine(item.ToString());
            }
        }
    }
}
