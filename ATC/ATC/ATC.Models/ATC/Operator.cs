using ATC.Abstractions;
using System;
using System.Collections.Generic;

namespace ATC.Models.ATC
{
    public class Operator
    {
        public event Action<IClient, int> RegistrationInBilling;

        public event Action<Terminal> RegestrarionInStation;

        private readonly IDictionary<Terminal, IClient> clientsTerminal;

        private readonly Random rnd = new Random();

        private int currenNumber;

        public Operator()
        {
            clientsTerminal = new Dictionary<Terminal, IClient>();
            currenNumber = rnd.Next(1000000, 9999999);
        }


        public Terminal Registarion(IClient client, int startBalance)
        {
            var terminal = new Terminal(currenNumber++);
            clientsTerminal.Add(terminal, client);
            RegistrationInBilling(client, startBalance);
            RegestrarionInStation(terminal);

            return terminal;
        }

        public IClient GetClient(Terminal terminal)
        {
            if (terminal == null)
            {
                throw new ArgumentNullException(nameof(terminal));
            }

            return clientsTerminal[terminal];
        }
    }
}
