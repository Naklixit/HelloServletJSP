//Tran Phuoc Duy 22CNTT2
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Net.Sockets;

public class Client
{

    private const int BUFFER_SIZE = 1024;
    private const int PORT_NUMBER = 9999;

    static ASCIIEncoding encoding = new ASCIIEncoding();

    public static void Main()
    {

        try
        {
            TcpClient client = new TcpClient();

            // 1. connect
            client.Connect("127.0.0.1", PORT_NUMBER);
            Stream stream = client.GetStream();

            Console.WriteLine("Server connected");
            
            while (true)
            {
                Console.Write("Enter a string (or type 'Exit' to quit): ");
                string str = Console.ReadLine();

                // 2. send
                byte[] data = encoding.GetBytes(str);
                stream.Write(data, 0, data.Length);
                
                if (str.Equals("Exit", StringComparison.OrdinalIgnoreCase))
                {
                    break; // Exit the loop if "Exit" is entered
                }

                // 3. receive
                data = new byte[BUFFER_SIZE];
                int size = stream.Read(data, 0, BUFFER_SIZE);

                Console.WriteLine("Server response: " + encoding.GetString(data, 0, size));
            }

            // 4. Close
            stream.Close();
            client.Close();
        }

        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex);
        }

        Console.Read();
    }
}
