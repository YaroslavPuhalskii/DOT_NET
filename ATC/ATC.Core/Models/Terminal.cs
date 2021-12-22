using ATC.Abstractions.ATC;
using System;

namespace ATC.Core.Models
{
    public class Terminal : ITerminal
    {
        public int Number { get; set; }

        public event Action<ITerminal, int> ActionCall;

        public event Action<ITerminal> ActionAnswer;

        public event Action<ITerminal> ActionReject;

        public event Action<ITerminal> ConnectToPort;

        public event Action<ITerminal> DisconnectToPort;

        public Terminal(int number)
        {
            Number = number;
        }

        public void Call(int number)
        {
            ActionCall?.Invoke(this, number);
        }

        public void Answer()
        {
            ActionAnswer?.Invoke(this);
        }

        public void Reject()
        {
            ActionReject?.Invoke(this);
        }

        public void Connect()
        {
            ConnectToPort?.Invoke(this);
        }

        public void Disconnect()
        {
            DisconnectToPort?.Invoke(this);
        }
    }
}
