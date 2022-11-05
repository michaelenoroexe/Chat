using System;

namespace UI.Connect
{
    internal interface IConnect<T>
    {
        /// <summary>
        /// Send message to remoute host.
        /// </summary>
        /// <param name="message">Type of object, sended to remoute host.</param>
        public void Send(T message);
        /// <summary>
        /// Occurs when message is received.
        /// </summary>
        public event EventHandler<MessageEventArgs<T>> MessageReceived;
    }
}
