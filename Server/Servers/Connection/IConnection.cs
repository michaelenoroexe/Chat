namespace Servers.Connection
{
    internal interface IConnection<T>
    {
        public void Send(T msg, IConnectUser<T> user);
    }
}
