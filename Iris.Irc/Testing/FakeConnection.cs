using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iris.Irc.Testing
{
    public class FakeConnection : IConnection
    {
        public Queue<string> LineQueue { get; set; }

        public Action<string> SendLineFunction { get; set; }

        public bool HasMoreLines
        {
            get { return LineQueue.Count > 0; }
        }

        public FakeConnection()
        {
            SendLineFunction = (line) => { };
            LineQueue = new Queue<string>();
        }

        public FakeConnection(Action<string> sendLineFunction)
        {
            this.SendLineFunction = sendLineFunction;
            LineQueue = new Queue<string>();
        }

        public void SendLine(string line)
        {
            SendLineFunction(line);
        }

        public bool Open()
        {
            //Just dummy.
            return true;
        }

        public void Close()
        {
            //Just dummy.
        }

        public bool TryGetNextLine(out string line)
        {
            line = LineQueue.Dequeue();
            return line != null;
        }

        public void DropConnectionUnexpectedly(Exception ex = null)
        {
            onConnectionDroppedUnexpectedly(ex);
        }

        protected void onConnectionDroppedUnexpectedly(Exception ex)
        {
            if (ConnectionDroppedUnexpectedly != null)
                ConnectionDroppedUnexpectedly(this, ex);
        }

        public event ConnectionDroppedUnexpectedlyEventHandler ConnectionDroppedUnexpectedly;
    }
}