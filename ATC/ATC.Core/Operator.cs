using ATC.Abstractions;
using ATC.Abstractions.ATC;
using ATC.Core.Models;
using System;
using System.Collections.Generic;

namespace ATC.Core
{
    public class Operator
    {
        public event Action<IClient, int> RegistrationInBilling;

        public event Action<ITerminal> RegistrationInStation;

        private readonly IDictionary<ITerminal, IClient> clientsTerminal;

        private int currenNumber = 1000000;

        public Operator()
        {
            clientsTerminal = new Dictionary<ITerminal, IClient>();
        }


        public ITerminal Registarion(IClient client, int startBalance)
        {
            ITerminal terminal = new Terminal(currenNumber++);
            clientsTerminal.Add(terminal, client);
            RegistrationInBilling?.Invoke(client, startBalance);
            RegistrationInStation?.Invoke(terminal);

            return terminal;
        }

        public IClient GetClient(ITerminal terminal)
        {
            if (terminal == null)
            {
                throw new ArgumentNullException($"{nameof(terminal)} can't be null!");
            }

            return clientsTerminal[terminal];
        }
    }
}
