using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TCPServer
{
    class Program
    {
        static void Main(string[] args)
        {
            int porta = 4000;

            while (true)
            {
                Console.Clear();
                Console.Write("Porta local: ");
                string texto = Console.ReadLine();

                try
                {
                    porta = Convert.ToInt32(texto);

                    if (porta > 65535)
                    {
                        Console.Write("\n\nFavor informar uma porta abaixo de 65535...");
                        Console.ReadKey();
                    }
                    else
                        break;
                }
                catch
                {
                    Console.Write("\n\nPorta inválida...");
                    Console.ReadKey();
                }
            }


            TcpListener server = new TcpListener(IPAddress.Loopback, porta);
            server.Start();

            Console.WriteLine("aguardando alguma conexao...");

            TcpClient client = server.AcceptTcpClient();

            string ip = ((IPEndPoint)client.Client.RemoteEndPoint).Address.ToString();

            Console.WriteLine("conexão estabelecida com {0}", ip);

            NetworkStream ns = client.GetStream();

            StreamReader reader = new StreamReader(ns);
            StreamWriter writer = new StreamWriter(ns);
            writer.AutoFlush = true;

            string line = string.Empty;
            string lineUpper = string.Empty;

            while (true)
            {
                try
                {
                    line = reader.ReadLine();
                    lineUpper = line.ToUpper();

                    writer.WriteLine(lineUpper);

                    Console.WriteLine("{0}: {1}", ip, lineUpper);
                }
                catch { }
            }
        }
    }
}
