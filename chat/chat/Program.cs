using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace chat
{
    class Program
    {

        private static Socket _clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
       
        private static void Connect()
        {
            int attempts = 0;

            while(!_clientSocket.Connected)
            {
                try
                {
                    attempts++;
                    _clientSocket.Connect(IPAddress.Loopback,51111);
                }
                catch(SocketException ex)
                {
                    Console.WriteLine(attempts);
                }
                
            }
            Console.Clear();
            Console.WriteLine("Connected ");
        }


        private static void Send()
        {

            new Thread(() => {
                while (true)
                {
                    byte[] recieveBuf = new byte[1024];
                    int rec = _clientSocket.Receive(recieveBuf);
                    byte[] data = new byte[rec];
                    Array.Copy(recieveBuf, data, rec);
                    Console.WriteLine();
                    Console.WriteLine("Recieve : " + Encoding.ASCII.GetString(data));
                    Console.WriteLine("\n Press enter to type your message \n");
                }
           
            }).Start();

            while(true)
            {
                
                Console.Write("Enter message : ");
                string req = Console.ReadLine();
                byte[] buffer = Encoding.ASCII.GetBytes(req);
                _clientSocket.Send(buffer);                            
            }
     
        }
        
        static void Main(string[] args)
        {
           Connect();
           Send();
            Console.ReadKey();
        }
    }
}
