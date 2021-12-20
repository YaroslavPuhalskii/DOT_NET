using ATC.Abstractions;
using ATC.Abstractions.ATC;
using ATC.Abstractions.ATC.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ATC.Models.ATC
{
    public class Station
    {
        public event Func<ITerminal, IClient> GetClient;

        public event Action<IClient, IClient, int> EndOfCall;

        private readonly ICollection<Tuple<ITerminal, ITerminal>> processingCall;

        private readonly ICollection<Tuple<ITerminal, int>> waitAnswer;

        private readonly IDictionary<ITerminal, IPort> terminalPort;

        private readonly Random rnd = new Random();

        private Queue<IPort> freePorts;

        public Station(int countPorts)
        {
            terminalPort = new Dictionary<ITerminal, IPort>();
            waitAnswer = new List<Tuple<ITerminal, int>>();
            processingCall = new List<Tuple<ITerminal, ITerminal>>();
            CreatePorts(countPorts);
        }

        public void Add(ITerminal terminal)
        {
            if (terminal != null)
            {
                BindtermianlWithPort(terminal);
            }
        }

        public void Remove(ITerminal terminal)
        {
            if (terminal != null)
            {
                UnBindTerminalWithPort(terminal);
            }
        }

        public void Call(ITerminal terminal, int number)
        {
            if (terminal == null)
            {
                throw new ArgumentNullException(nameof(terminal));
            }

            waitAnswer.Add(new Tuple<ITerminal, int>(terminal, number));
        }

        public void Answer(ITerminal terminal)
        {
            if (terminal == null)
            {
                throw new ArgumentNullException(nameof(terminal));
            }

            var call = waitAnswer.FirstOrDefault(x => x.Item2 == terminal.Number);

            if (call == null)
            {
                throw new ArgumentNullException(nameof(call));
            }

            processingCall.Add(new Tuple<ITerminal, ITerminal>(call.Item1, terminal));

            waitAnswer.Remove(call);
        }

        public void Reject(ITerminal terminal)
        {
            if (terminal == null)
            {
                throw new ArgumentNullException(nameof(terminal));
            }

            var call = processingCall.FirstOrDefault(x => x.Item1.Number == terminal.Number
                                                        || x.Item2.Number == terminal.Number);

            if (call == null)
            {
                throw new ArgumentNullException(nameof(call));
            }

            var port1 = terminalPort[call.Item1];
            var port2 = terminalPort[call.Item2];
            port1.PortStatus = port2.PortStatus = PortStatus.Online;

            processingCall.Remove(call);

            var caller = GetClient(call.Item1);
            var receiver = GetClient(call.Item2);

            var time = rnd.Next(1, 3600);

            EndOfCall(caller, receiver, time);
        }

        private void BindtermianlWithPort(ITerminal terminal)
        {
            IPort port = freePorts.Dequeue();

            if (port == null)
            {
                throw new ArgumentNullException(nameof(port));
            }

            terminal.ActionCall += (number) => port.Call(terminal, number);
            terminal.ActionAnswer += () => port.Answer(terminal);
            terminal.ActionReject += () => port.Reject(terminal);

            terminal.ConnectToPort += () => port.Link(terminal);
            terminal.DisconnectToPort += () => port.Unlink(terminal);

            port.PortStatus = PortStatus.Online;

            terminalPort.Add(terminal, port);
        }

        private void UnBindTerminalWithPort(ITerminal terminal)
        {
            var port = terminalPort[terminal];

            if (port == null)
            {
                throw new ArgumentNullException(nameof(port));
            }

            terminal.ActionCall -= (number) => port.Call(terminal, number);
            terminal.ActionAnswer -= () => port.Answer(terminal);
            terminal.ActionReject -= () => port.Reject(terminal);

            terminal.ConnectToPort -= () => port.Link(terminal);
            terminal.DisconnectToPort -= () => port.Unlink(terminal);

            port.PortStatus = PortStatus.Offline;

            terminalPort.Remove(terminal);
            freePorts.Enqueue(port);
        }

        private void CreatePorts(int countPorts)
        {
            freePorts = new Queue<IPort>();

            for (int i = 0; i < countPorts; i++)
            {
                IPort port = new Port(i);
                port.ActionCall += Call;
                port.ActionAnswer += Answer;
                port.ActionReject += Reject;

                port.LinkTerminal += Add;
                port.UnlinkTerminal += Remove;

                freePorts.Enqueue(port);
            }
        }
    }
}
