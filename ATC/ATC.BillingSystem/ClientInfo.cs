using ATC.Abstractions;

namespace ATC.BillingSystem
{
    public class ClientInfo
    {
        public IClient Client { get; }

        public decimal Balance { get; set; }

        public ClientInfo(IClient client, decimal balance)
        {
            Client = client;
            Balance = balance;
        }
    }
}
