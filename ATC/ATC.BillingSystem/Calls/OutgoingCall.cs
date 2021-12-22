using ATC.Abstractions;
using System;
using System.Text;

namespace ATC.BillingSystem.Calls
{
    public class OutgoingCall : Call
    {
        private StringBuilder builder;

        public decimal Price { get; }

        public OutgoingCall(IClient caller, IClient receiver, DateTime dateTime, int time, decimal price)
            : base(caller, receiver, dateTime, time)
        {
            Price = price;
        }

        public override string ToString()
        {
            builder = builder ?? new StringBuilder();
            builder.Clear();

            builder.AppendLine($"Outgoing call : {Caller} - > {Receiver}");
            builder.AppendLine($"Date : {DateTime}");
            builder.AppendLine($"Time : {Time}s.");
            builder.AppendLine($"Price : {Price}");

            return builder.ToString();
        }
    }
}
