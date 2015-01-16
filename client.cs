using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace appclient
{
    class Program
    {
        static void Main(string[] args)
        {

            var hubConnection = new HubConnection("http://signals-1.apphb.com/"); // подключение к серверу 
            IHubProxy HubProxy = hubConnection.CreateHubProxy("MainHub"); // соединение с классом-хабом, методы которого будут использоваться клиентами
            hubConnection.Start().Wait(); // старт соединения (обязательно нужно ждать стабилизации соединения, чтобы методы не вызывались раньше установления соединения)

            HubProxy.Invoke("SendMessage", "Client connected"  ); // вызов метода "SendMessage" на стороне сервера и передача параметра "Client connected"

            HubProxy.On("newMessage", x => { // метод срабатывает, когда сервер отправляет данные на клиент
                Console.WriteLine(x);
            });


        }
    }
}
