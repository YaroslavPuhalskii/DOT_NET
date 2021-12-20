using ATC.Abstractions;
using ATC.BillingSystem.Calls;
using System.Collections.Generic;
using System.Linq;

namespace ATC.BillingSystem.Reports
{
    public class CallReport
    {
        public IEnumerable<OutgoingCall> SortByPrice(Billing billing, IClient client)
        {
            return billing.GetOutgoingCalls.Where(x => x.caller == client)
                                           .OrderBy(x => x.Price);
        }

        public IEnumerable<OutgoingCall> SortByDate(Billing billing, IClient client)
        {
            return billing.GetOutgoingCalls.Where(x => x.caller == client)
                                           .OrderBy(x => x.DateTime);
        }

        public IEnumerable<OutgoingCall> GetCallReport(Billing billing, IClient client)
        {
            return billing.GetOutgoingCalls.Where(x => x.caller == client);
        }

        public decimal GetBalance(Billing billing, IClient client)
        {
            return billing.GetClients.FirstOrDefault(x => x.client == client).Balance;
        }
    }
}
