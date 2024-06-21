using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

class ServerSync
{
    static void Main(string[] args)
    {
        System.Console.OutputEncoding = Encoding.Unicode;

        // Створюємо серверний сокет
        TcpListener server = new TcpListener(IPAddress.Any, 12345);
        server.Start();
        Console.WriteLine("Сервер запущено...");

        while (true)
        {
            // Приймаємо підключення від клієнта
            TcpClient client = server.AcceptTcpClient();
            NetworkStream stream = client.GetStream();

            // Читаємо дані від клієнта
            byte[] buffer = new byte[256];
            int bytesRead = stream.Read(buffer, 0, buffer.Length);
            string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
            string clientIP = ((IPEndPoint)client.Client.RemoteEndPoint).Address.ToString();
            Console.WriteLine($"О {DateTime.Now:HH:mm} від {clientIP} отримано рядок: {message}");

            // Відправляємо відповідь клієнту
            string response = "Привіт, клієнте!";
            byte[] responseData = Encoding.UTF8.GetBytes(response);
            stream.Write(responseData, 0, responseData.Length);

            // Закриваємо підключення
            stream.Close();
            client.Close();
        }
    }
}
