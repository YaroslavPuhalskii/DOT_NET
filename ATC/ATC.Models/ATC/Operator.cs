using ATC.Abstractions;
using ATC.Abstractions.ATC;
using System;
using System.Collections.Generic;

namespace ATC.Models.ATC
{
    public class Operator
    {
        public event Action<IClient, int> RegistrationInBilling;

        public event Action<ITerminal> RegestrationInStation;

        private readonly IDictionary<ITerminal, IClient> clientsTerminal;

        private readonly Random rnd = new Random();

        private int currenNumber;

        public Operator()
        {
            clientsTerminal = new Dictionary<ITerminal, IClient>();
            currenNumber = rnd.Next(1000000, 9999999);
        }


        public ITerminal Registarion(IClient client, int startBalance)
        {
            ITerminal terminal = new Terminal(currenNumber++);
            clientsTerminal.Add(terminal, client);
            RegistrationInBilling(client, startBalance);
            RegestrationInStation(terminal);

            return terminal;
        }

        public IClient GetClient(ITerminal terminal)
        {
            if (terminal == null)
            {
                throw new ArgumentNullException(nameof(terminal));
            }

            return clientsTerminal[terminal];
        }
    }
}
