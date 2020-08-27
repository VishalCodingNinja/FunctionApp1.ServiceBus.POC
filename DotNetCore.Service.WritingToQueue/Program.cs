using System.Threading.Tasks;
using System.Text;
using Microsoft.Azure.ServiceBus;

namespace DotNetCore.Service.WritingToQueue
{
    class Program
    {
        static QueueClient queueClient = new QueueClient("Endpoint=sb://service-bus-poc-1.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=Su++fkdVAjazBG89+TXa05ruzEoTxqOAdyl6FS9CeBc=", "msgqueue");
        public static void Main(string[] args)
        {
            MainAsync().GetAwaiter().GetResult();
        }

        static async Task MainAsync()
        {
            string message = "Sure would like a large pepperoni!";
            var encodedMessage = new Message(Encoding.UTF8.GetBytes(message));
            await queueClient.SendAsync(encodedMessage);
        }
    }
}
