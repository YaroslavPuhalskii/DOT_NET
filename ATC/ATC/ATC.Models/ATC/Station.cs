using ATC.Abstractions;
using ATC.Models.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ATC.Models.ATC
{
    public class Station
    {
        public event Func<Terminal, IClient> GetClient;

        public event Action<IClient, IClient, int> EndOfCall;

        private readonly ICollection<Tuple<Terminal, Terminal>> processingCall;

        private readonly ICollection<Tuple<Terminal, int>> waitAnswer;

        private readonly IDictionary<Terminal, Port> terminalPort;

        private Queue<Port> freePorts;

        public Station(int countPorts)
        {
            terminalPort = new Dictionary<Terminal, Port>();
            waitAnswer = new List<Tuple<Terminal, int>>();
            processingCall = new List<Tuple<Terminal, Terminal>>();
            CreatePorts(countPorts);
        }

        public void BindtermianlWithPort(Terminal terminal)
        {
            if (terminal == null)
            {
                throw new ArgumentNullException(nameof(terminal));
            }

            var port = freePorts.Dequeue();

            if (port == null)
            {
                throw new ArgumentNullException(nameof(port));
            }

            terminal.ActionCall += port.Call;
            terminal.ActionAnswer += port.Answer;
            terminal.ActionReject += port.Reject;

            port.PortStatus = PortStatus.Online;

            terminalPort.Add(terminal, port);
        }

        public void UnBindTerminalWithPort(Terminal terminal)
        {
            if (terminal == null)
            {
                throw new ArgumentNullException(nameof(terminal));
            }

            var port = terminalPort[terminal];

            if (port == null)
            {
                throw new ArgumentNullException(nameof(port));
            }

            terminal.ActionCall -= port.Call;
            terminal.ActionAnswer -= port.Answer;
            terminal.ActionReject -= port.Reject;

            port.PortStatus = PortStatus.Offline;

            terminalPort.Remove(terminal);
            freePorts.Enqueue(port);
        }

        private void Call(Terminal terminal, int number)
        {
            if (terminal == null)
            {
                throw new ArgumentNullException(nameof(terminal));
            }

            var port = terminalPort[terminal];
            port.PortStatus = PortStatus.Busy;

            waitAnswer.Add(new Tuple<Terminal, int>(terminal, number));
        }

        private void Answer(Terminal terminal)
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

            var port = terminalPort[terminal];
            port.PortStatus = PortStatus.Busy;

            processingCall.Add(new Tuple<Terminal, Terminal>(call.Item1, terminal));

            waitAnswer.Remove(call);
        }

        private void Reject(Terminal terminal)
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

            Random rnd = new Random();
            var time = rnd.Next(1, 60);

            EndOfCall(caller, receiver, time);
        }

        private void CreatePorts(int countPorts)
        {
            freePorts = new Queue<Port>();

            for (int i = 0; i < countPorts; i++)
            {
                var port = new Port(i);
                port.ActionCall += Call;
                port.ActionAnswer += Answer;
                port.ActionReject += Reject;

                freePorts.Enqueue(port);
            }
        }
    }
}
