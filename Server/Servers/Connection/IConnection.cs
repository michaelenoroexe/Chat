namespace Servers.Connection
{
    /// <summary>
    /// Connection between two users.
    /// </summary>
    /// <typeparam name="T">Content type of messages.</typeparam>
    internal interface IConnection<T>
    {
        /// <summary>
        /// Send message to oposite user of sender.
        /// </summary>
        /// <param name="msg">Content of message.</param>
        /// <param name="user">Initializer of message sending.</param>
        public void Send(T msg, IConnectUser<T> user);
    }
}
