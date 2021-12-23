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

        private readonly ICollection<OutgoingCall> calls;

        private readonly ITariffPlan tariffPlan;

        public IEnumerable<OutgoingCall> GetOutgoingCalls => calls;

        public Billing(ITariffPlan tariffPlan)
        {
            this.tariffPlan = tariffPlan;
            calls = new List<OutgoingCall>();
            clients = new List<ClientInfo>();
        }

        public void Registration(IClient client, int startBalance)
        {
            if (client == null)
            {
                throw new ArgumentNullException($"{nameof(client)} can't be null");
            }

            if (startBalance < 0)
            {
                throw new ArgumentOutOfRangeException($"{nameof(startBalance)} can't be less 0!");
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

            WritingOffMoney(caller, cost);

            calls.Add(new OutgoingCall(caller, receiver, DateTime.Now, time, cost));
        }

        private void WritingOffMoney(IClient caller, decimal money)
        {
            var client = clients.FirstOrDefault(x => x.Client == caller);

            if (client == null)
            {
                throw new ArgumentNullException($"{nameof(client)} can't be null!");
            }

            client.Balance -= money;
        }
    }
}
