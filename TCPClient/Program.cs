using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TCPClient
{
    class Program
    {
        static void Main(string[] args)
        {
            string ipDoServidor = "127.0.0.1";

            while (true)
            {
                Console.Clear();
                Console.Write("IP do servidor: ");

                ipDoServidor = Console.ReadLine();

                if (!IsIPv4(ipDoServidor))
                {
                    Console.Write("\n\nIP Inválido...");
                    Console.ReadKey();
                }
                else
                    break;
            }

            int porta = 4000;

            while (true)
            {
                Console.Clear();
                Console.WriteLine("IP do servidor: {0}", ipDoServidor);
                Console.Write("Porta do servidor: ");
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

            TcpClient client = new TcpClient(ipDoServidor, porta);

            NetworkStream ns = client.GetStream();

            StreamReader reader = new StreamReader(ns);
            StreamWriter writer = new StreamWriter(ns);
            writer.AutoFlush = true;

            while (true)
            {
                Console.Write("msg: ");
                string texto = Console.ReadLine();

                writer.WriteLine(texto);

                string textoConvertido = reader.ReadLine();
                Console.WriteLine("resposta: " + textoConvertido);
            }
        }

        static bool IsIPv4(string value)
        {
            var quads = value.Split('.');

            // if we do not have 4 quads, return false
            if (!(quads.Length == 4)) return false;

            // for each quad
            foreach (var quad in quads)
            {
                int q;
                // if parse fails 
                // or length of parsed int != length of quad string (i.e.; '1' vs '001')
                // or parsed int < 0
                // or parsed int > 255
                // return false
                if (!Int32.TryParse(quad, out q)
                    || !q.ToString().Length.Equals(quad.Length)
                    || q < 0
                    || q > 255) { return false; }
            }

            return true;
        }
    }
}
