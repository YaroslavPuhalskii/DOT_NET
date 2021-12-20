using ATC.Abstractions.ATC;
using System;

namespace ATC.Models.ATC
{
    public class Terminal : ITerminal
    {
        public int Number { get; set; }

        public event Action<int> ActionCall;

        public event Action ActionAnswer;

        public event Action ActionReject;

        public event Action ConnectToPort;

        public event Action DisconnectToPort;

        public Terminal(int number)
        {
            Number = number;
        }

        public void Call(int number)
        {
            ActionCall?.Invoke(number);
        }

        public void Answer()
        {
            ActionAnswer?.Invoke();
        }

        public void Reject()
        {
            ActionReject?.Invoke();
        }

        public void Connect()
        {
            ConnectToPort?.Invoke();
        }

        public void Disconnect()
        {
            DisconnectToPort?.Invoke();
        }
    }
}
