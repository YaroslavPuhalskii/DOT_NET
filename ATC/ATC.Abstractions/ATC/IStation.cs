using System;

namespace ATC.Abstractions.ATC
{
    public interface IStation
    {
        event Func<ITerminal, IClient> GetClient;

        event Action<IClient, IClient, int> EndOfCall;

        void Add(ITerminal terminal);

        void Remove(ITerminal terminal);
    }
}
