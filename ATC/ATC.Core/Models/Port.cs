using ATC.Abstractions.ATC;
using ATC.Abstractions.ATC.Specifications;
using System;

namespace ATC.Core.Models
{
    public class Port : IPort
    {
        public int Number { get; }

        public PortStatus PortStatus { get; set; }

        public event Action<ITerminal, int> ActionCall;

        public event Action<ITerminal> ActionAnswer;

        public event Action<ITerminal> ActionReject;

        public event Action<ITerminal> LinkTerminal;

        public event Action<ITerminal> UnlinkTerminal;

        public Port(int number)
        {
            Number = number;
        }

        public void Call(ITerminal terminal, int number)
        {
            if (PortStatus == PortStatus.Online)
            {
                PortStatus = PortStatus.Busy;
                ActionCall?.Invoke(terminal, number);
            }
        }

        public void Answer(ITerminal terminal)
        {
            if (PortStatus == PortStatus.Online)
            {
                PortStatus = PortStatus.Busy;
                ActionAnswer?.Invoke(terminal);
            }
        }

        public void Reject(ITerminal terminal)
        {
            if (PortStatus == PortStatus.Busy)
            {
                ActionReject?.Invoke(terminal);
            }
        }

        public void Link(ITerminal terminal)
        {
            LinkTerminal?.Invoke(terminal);
        }

        public void Unlink(ITerminal terminal)
        {
            UnlinkTerminal?.Invoke(terminal);
        }
    }
}
