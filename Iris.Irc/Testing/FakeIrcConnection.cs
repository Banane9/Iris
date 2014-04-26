using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iris.Irc.Testing
{
    public class FakeIrcConnection : IConnection
    {
        public Action<string> sendLineFunction { get; set; }

        public Action startFunction { get; set; }

        public Action stopFunction { get; set; }

        public FakeIrcConnection()
        {
            startFunction = () => { };
            stopFunction = () => { };
            sendLineFunction = (line) => { };
        }

        public FakeIrcConnection(Action startFunction, Action stopFunction, Action<string> sendLineFunction)
        {
            this.startFunction = startFunction;
            this.stopFunction = stopFunction;
            this.sendLineFunction = sendLineFunction;
        }

        public event EventHandler ConnectionClosed;

        public event EventHandler ConnectionClosing;

        public event NewLineEventHandler NewLine;

        public void SendConnectionClosedEvent()
        {
            if (ConnectionClosed != null) ConnectionClosed(this, EventArgs.Empty);
        }

        public void SendConnectionClosingEvent()
        {
            if (ConnectionClosing != null) ConnectionClosing(this, EventArgs.Empty);
        }

        public void SendLine(string line)
        {
            sendLineFunction(line);
        }

        public void SendNewLineEvent(string line)
        {
            if (NewLine != null) NewLine(this, line);
        }

        public void Open()
        {
            startFunction();
        }

        public void Close()
        {
            stopFunction();
        }
    }
}