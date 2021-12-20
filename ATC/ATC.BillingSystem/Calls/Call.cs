using ATC.Abstractions;
using System;

namespace ATC.BillingSystem.Calls
{
    public abstract class Call
    {
        public IClient caller;

        public IClient receiver;

        public DateTime DateTime { get; }

        public int time;

        public Call(IClient caller, IClient receiver, DateTime DateTime, int time)
        {
            this.caller = caller;
            this.receiver = receiver;
            this.DateTime = DateTime;
            this.time = time;
        }
    }
}
