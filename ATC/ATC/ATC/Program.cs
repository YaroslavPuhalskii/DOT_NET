using ATC.Abstractions;
using ATC.Core.BillingSystem;
using ATC.Core.TariffPlan;
using ATC.Models;
using ATC.Models.ATC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            @operator.RegestrarionInStation += station.BindtermianlWithPort;
            station.EndOfCall += billing.EndOfCall;
            station.GetClient += @operator.GetClient;

            IClient c1 = new Client("Selena", "Gomez");
            IClient c2 = new Client("Maxim", "Swift");

            var t1 = @operator.Registarion(c1, 1500);
            var t2 = @operator.Registarion(c2, 1200);

            t1.Call(t2.Number);
            t2.Answer();
            t1.Reject();

            Console.ReadLine();
        }
    }
}
