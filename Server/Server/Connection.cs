using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    internal class Connection
    {
        /// <summary>
        /// IP adress of first user in connection
        /// </summary>
        public IPAddress FirstUser { get; }
        /// <summary>
        /// IP adress of second user in connection
        /// </summary>
        public IPAddress SecondUser { get; }

        public Connection(IPAddress firstUser, IPAddress secondUser)
        {
            FirstUser = firstUser;

            SecondUser = secondUser;
        }
        public Connection(string firstUser, string secondUser)
        {
            FirstUser = IPAddress.Parse(firstUser);

            SecondUser = IPAddress.Parse(secondUser);
        }

        /// <summary>
        /// Determine when specific Connection object are equal to current
        /// </summary>
        /// <param name="con"></param>
        /// <returns></returns>
        public bool Equals(Connection con)
        {
            return FirstUser == con.FirstUser && SecondUser == con.SecondUser;
        }
        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (obj.GetType() != typeof(Connection)) return false;
            return this.Equals((Connection)obj);
        }
        public override int GetHashCode()
        {
            return FirstUser.GetHashCode() + SecondUser.GetHashCode();
        }
    }
}
