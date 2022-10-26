namespace Servers.Connection
{
    internal interface IConnectUser<T>
    {
        public void SetConnection(IConnection<T> con);

        public void Notify(T message);
    }
}
