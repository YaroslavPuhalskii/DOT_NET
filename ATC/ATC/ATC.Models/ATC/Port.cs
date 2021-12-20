using ATC.Models.Specifications;
using System;

namespace ATC.Models.ATC
{
    public class Port
    {
        public int Number { get; }

        public PortStatus PortStatus { get; set; }

        public event Action<Terminal, int> ActionCall;

        public event Action<Terminal> ActionAnswer;

        public event Action<Terminal> ActionReject;

        public Port(int number)
        {
            Number = number;
        }

        public void Call(Terminal terminal, int number)
        {
            if (PortStatus == PortStatus.Online)
            {
                ActionCall?.Invoke(terminal, number);
            }
        }

        public void Answer(Terminal terminal)
        {
            if (PortStatus == PortStatus.Online)
            {
                ActionAnswer?.Invoke(terminal);
            }
        }

        public void Reject(Terminal terminal)
        {
            if (PortStatus == PortStatus.Busy)
            {
                ActionReject?.Invoke(terminal);
            }
        }
    }
}
