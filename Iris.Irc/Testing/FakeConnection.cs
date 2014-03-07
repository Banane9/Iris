using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iris.Irc.Testing
{
    public class FakeConnection : IConnection
    {
        public Action<string> sendLineFunction { get; set; }

        public Action startFunction { get; set; }

        public Action stopFunction { get; set; }

        public FakeConnection()
        {
            startFunction = () => { };
            stopFunction = () => { };
            sendLineFunction = (line) => { };
        }

        public FakeConnection(Action startFunction, Action stopFunction, Action<string> sendLineFunction)
        {
            this.startFunction = startFunction;
            this.stopFunction = stopFunction;
            this.sendLineFunction = sendLineFunction;
        }

        public event ConnectionClosedEventHandler ConnectionClosed;

        public event ConnectionClosingEventHandler ConnectionClosing;

        public event NewLineEventHandler NewLine;

        public void SendConnectionClosedEvent()
        {
            if (ConnectionClosed != null) ConnectionClosed(this);
        }

        public void SendConnectionClosingEvent()
        {
            if (ConnectionClosing != null) ConnectionClosing(this);
        }

        public void SendLine(string line)
        {
            sendLineFunction(line);
        }

        public void SendNewLineEvent(string line)
        {
            if (NewLine != null) NewLine(this, line);
        }

        public void Start()
        {
            startFunction();
        }

        public void Stop()
        {
            stopFunction();
        }
    }
}