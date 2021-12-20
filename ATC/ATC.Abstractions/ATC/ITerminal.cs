using System;

namespace ATC.Abstractions.ATC
{
    public interface ITerminal
    {
        int Number { get; }

        event Action<int> ActionCall;

        event Action ActionAnswer;

        event Action ActionReject;

        event Action ConnectToPort;

        event Action DisconnectToPort;

        void Call(int number);

        void Answer();

        void Reject();

        void Connect();

        void Disconnect();
    }
}
