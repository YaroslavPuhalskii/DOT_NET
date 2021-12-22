using ATC.Abstractions;
using System;

namespace ATC.BillingSystem.Calls
{
    public abstract class Call
    {
        public IClient Caller { get; }

        public IClient Receiver { get; }

        public DateTime DateTime { get; }

        public int Time { get; }

        protected Call(IClient caller, IClient receiver, DateTime dateTime, int time)
        {
            Caller = caller;
            Receiver = receiver;
            DateTime = dateTime;
            Time = time;
        }
    }
}
