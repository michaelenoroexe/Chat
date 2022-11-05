namespace Servers.Connection
{
    internal interface IConnectUser<T>
    {
        /// <summary>
        /// Set connection to current user.
        /// </summary>
        /// <param name="con">Inctance of connection.</param>
        public void SetConnection(IConnection<T> con);
        /// <summary>
        /// Receive message from another user.
        /// </summary>
        /// <param name="message">Content of message.</param>
        public void Notify(T message);
    }
}
