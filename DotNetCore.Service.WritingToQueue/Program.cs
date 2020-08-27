using System.Threading.Tasks;
using System.Text;
using Microsoft.Azure.ServiceBus;

namespace DotNetCore.Service.WritingToQueue
{
    class Program
    {
        static QueueClient queueClient = new QueueClient("your-service-bus-namespace", "your-queue-name");
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
