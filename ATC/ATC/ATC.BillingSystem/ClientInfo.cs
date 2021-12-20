using ATC.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATC.BillingSystem
{
    public class ClientInfo
    {
        public IClient client;

        public decimal Balance { get; set; }

        public ClientInfo(IClient client, decimal balance)
        {
            this.client = client;
            Balance = balance;
        }
    }
}
