using ATC.Abstractions;
using ATC.BillingSystem.Calls;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ATC.BillingSystem.Reports
{
    public class CallReport
    {
        public IEnumerable<OutgoingCall> SortByPrice(Billing billing, IClient client)
        {
            if (billing == null || client == null)
            {
                throw new ArgumentNullException($"{nameof(billing)} or {nameof(client)} is null!");
            }

            return billing.GetOutgoingCalls.Where(x => x.Caller == client)
                                           .OrderBy(x => x.Price);
        }

        public IEnumerable<OutgoingCall> SortByDate(Billing billing, IClient client)
        {
            if (billing == null || client == null)
            {
                throw new ArgumentNullException($"{nameof(billing)} or {nameof(client)} is null!");
            }

            return billing.GetOutgoingCalls.Where(x => x.Caller == client)
                                           .OrderBy(x => x.DateTime);
        }

        public IEnumerable<OutgoingCall> SortByReceiver(Billing billing, IClient client)
        {
            if (billing == null || client == null)
            {
                throw new ArgumentNullException($"{nameof(billing)} or {nameof(client)} is null!");
            }

            return billing.GetOutgoingCalls.Where(x => x.Caller == client)
                                           .OrderBy(x => x.Receiver.FirstName);
        }


        public IEnumerable<OutgoingCall> GetCallReport(Billing billing, IClient client)
        {
            if (billing == null || client == null)
            {
                throw new ArgumentNullException($"{nameof(billing)} or {nameof(client)} is null!");
            }

            return billing.GetOutgoingCalls.Where(x => x.Caller == client);
        }
    }
}
