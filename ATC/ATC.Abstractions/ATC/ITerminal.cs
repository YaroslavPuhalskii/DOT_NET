using System;

namespace ATC.Abstractions.ATC
{
    public interface ITerminal
    {
        int Number { get; }

        event Action<ITerminal, int> ActionCall;

        event Action<ITerminal> ActionAnswer;

        event Action<ITerminal> ActionReject;

        event Action<ITerminal> ConnectToPort;

        event Action<ITerminal> DisconnectToPort;

        void Call(int number);

        void Answer();

        void Reject();

        void Connect();

        void Disconnect();
    }
}
