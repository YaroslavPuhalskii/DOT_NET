using ATC.Abstractions;

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
