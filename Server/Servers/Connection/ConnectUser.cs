namespace Servers.Connection
{
    /// <summary>
    /// User with inplemented connection system.
    /// </summary>
    internal abstract class ConnectUser<T> : IConnectUser<T>
    {
        protected IConnection<T>? connect = null;
        /// <summary>
        /// Set established connection to property.
        /// </summary>
        /// <param name="con"></param>
        public virtual void SetConnection(IConnection<T> con)
        {
            connect = con;
        }
        /// <summary>
        /// Send message to anouther user with established connection.
        /// </summary>
        /// <param name="message">Message that will be sended to opposite user of connection.</param>
        /// <exception cref="InvalidOperationException">If the connection is not established.</exception>
        protected virtual void Send(T message)
        {
            if (connect == null) throw new InvalidOperationException("Connection is not assign");
            connect.Send(message, this);
        }
        /// <summary>
        /// Receive message from another user.
        /// </summary>
        /// <param name="message">Content of message.</param>
        public abstract void Notify(T message);
    }
}
