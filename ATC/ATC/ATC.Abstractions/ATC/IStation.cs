using System;

namespace ATC.Abstractions.ATC
{
    public interface IStation
    {
        event Func<ITerminal, IClient> GetClient;

        void Add(ITerminal terminal);

        void Remove(ITerminal terminal);

        void Call(ITerminal terminal);

        void Answer(ITerminal terminal);
    }
}
