using ATC.Abstractions;
using ATC.Abstractions.BillingSystem.TariffPlan;
using ATC.BillingSystem.Calls;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ATC.BillingSystem
{
    public class Billing
    {
        private readonly ICollection<ClientInfo> clients;

        private readonly ICollection<Call> calls;

        private readonly ITariffPlan tariffPlan;

        public IEnumerable<OutgoingCall> GetOutgoingCalls => calls.OfType<OutgoingCall>();

        public IEnumerable<Call> GetCalls => calls;

        public IEnumerable<ClientInfo> GetClients => clients;

        public Billing(ITariffPlan tariffPlan)
        {
            this.tariffPlan = tariffPlan;
            calls = new List<Call>();
            clients = new List<ClientInfo>();
        }

        public void Registration(IClient client, int startBalance)
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            if (startBalance < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(startBalance));
            }

            clients.Add(new ClientInfo(client, startBalance));
        }

        public void EndOfCall(IClient caller, IClient receiver, int time)
        {
            if (caller == null || receiver == null)
            {
                throw new ArgumentNullException($"{nameof(caller)} or {nameof(receiver)} is null!");
            }

            var cost = tariffPlan.GetPrice(time);

            WrittingOffMoney(caller, cost);

            calls.Add(new OutgoingCall(caller, receiver, DateTime.Now, time, cost));
        }

        private void WrittingOffMoney(IClient caller, decimal money)
        {
            var client = clients.FirstOrDefault(x => x.client == caller);
            client.Balance -= money;
        }
    }
}
