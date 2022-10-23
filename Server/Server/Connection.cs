using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    internal class Connection
    {
        /// <summary>
        /// IP adress of first user in connection
        /// </summary>
        public UserRequest FirstUser { get; }
        /// <summary>
        /// IP adress of second user in connection
        /// </summary>
        public UserRequest SecondUser { get; }

        public Connection(UserRequest firstUser, UserRequest secondUser)
        {
            FirstUser = firstUser;

            SecondUser = secondUser;
        }

        public void Send(UserRequest client)
        {
            if (client == FirstUser)
            {
                ((TcpClient)SecondUser).GetStream().Write(client.SendedTextBytes);
            }
            else
            {
                ((TcpClient)FirstUser).GetStream().Write(client.SendedTextBytes);
            }
        }

        /// <summary>
        /// Determine when specific Connection object are equal to current
        /// </summary>
        /// <param name="con"></param>
        /// <returns></returns>
        public bool Equals(Connection con)
        {
            return this.GetHashCode() == con.GetHashCode() && SecondUser == con.SecondUser;
        }
        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (obj.GetType() != typeof(Connection)) return false;
            return this.Equals((Connection)obj);
        }
        public override int GetHashCode()
        {
            return FirstUser.IP.ToString().GetHashCode() + SecondUser.IP.ToString().GetHashCode();
        }
    }
}
