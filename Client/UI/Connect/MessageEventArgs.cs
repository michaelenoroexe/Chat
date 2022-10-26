using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.Connect
{
    internal class MessageEventArgs<T> : EventArgs
    {
        /// <summary>
        /// Message received from remoute host.
        /// </summary>
        public T Message { get; private set; }
        /// <summary>
        /// Initialize message.
        /// </summary>
        /// <param name="message">Content of receiving message.</param>
        public MessageEventArgs(T message)
        {
            Message = message;
        }
    }
}
