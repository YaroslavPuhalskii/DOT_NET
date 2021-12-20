using System;

namespace ATC.Models.ATC
{
    public class Terminal
    {
        public int Number { get; set; }

        public event Action<Terminal, int> ActionCall;

        public event Action<Terminal> ActionAnswer;

        public event Action<Terminal> ActionReject;

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
    }
}
