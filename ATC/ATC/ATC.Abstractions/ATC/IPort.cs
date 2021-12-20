using ATC.Abstractions.ATC.Specifications;
using System;

namespace ATC.Abstractions.ATC
{
    public interface IPort
    {
        int Number { get; }

        PortStatus PortStatus { get; set; }

        event Action<ITerminal, int> ActionCall;

        event Action<ITerminal> ActionAnswer;

        event Action<ITerminal> ActionReject;

        event Action<ITerminal> LinkTerminal;

        event Action<ITerminal> UnlinkTerminal;

        void Call(ITerminal terminal, int number);

        void Answer(ITerminal terminal);

        void Reject(ITerminal terminal);

        void Link(ITerminal terminal);

        void Unlink(ITerminal terminal);
    }
}
