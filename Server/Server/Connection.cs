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

            //SecondUser = secondUser;

            SecondUser = firstUser;
            Task.Run(() => { Thread.Sleep(3000); var s = new StreamWriter((Stream)FirstUser); s.WriteLine("ooooooh, hhhhhi"); s.Flush(); });
            Task.Run(Brodcast);
        }
        // Mediator pattren sender
        public void Send(UserRequest client)
        {
            if (client == FirstUser)
            {
                Console.WriteLine(client.SendedTextString);
                var ssw = new StreamWriter(((Stream)SecondUser));
                ssw.WriteLine(new StreamReader((Stream)client).ReadLine());
                ssw.Flush();
                ((Stream)SecondUser).Flush();
            }
            else
            {
                ((Stream)FirstUser).Write(client.SendedTextBytes);
                ((Stream)FirstUser).Flush();
            }
        }
        /// <summary>
        /// Brodcast messages between users
        /// </summary>
        /// <returns></returns>
        public async void Brodcast()
        {
            var t1 = Task.Run(() => Brodcast(FirstUser));
            var t2 = Task.Run(() => Brodcast(SecondUser));

            Task.WaitAll(t1, t2);
        }
        //Read user stream and send to anouther user
        private async void Brodcast(UserRequest ur)
        {
            StreamReader sr = null;
            try
            {
                string? res ;
                sr = new StreamReader((Stream)ur);
                while (true)
                {
                    res = sr.ReadLine();
                    Send(ur);
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                sr?.Dispose();
                ur.Dispose();
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
