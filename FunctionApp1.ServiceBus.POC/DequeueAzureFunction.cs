using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace DequeueAzureFunction.ServiceBus.POC
{
    public static class DequeueAzureFunction
    {
        static QueueClient queueClient = new QueueClient("your-service-bus-namespace", "your-queue-name");
        [FunctionName("Function1")]
        public static void Run([ServiceBusTrigger("msgqueue", Connection = "AzureWebJobsStorageQueueStorage")]string myQueueItem, ILogger log)
        {
            MainAsync().GetAwaiter().GetResult();

        }

        static async Task MainAsync()
        {
          
            var messageHandlerOptions = new MessageHandlerOptions(ExceptionReceivedHandler)
            {
                MaxConcurrentCalls = 1,
                AutoComplete = false
            };
            queueClient.RegisterMessageHandler(MessageHandler, messageHandlerOptions);
        }

        private static async Task MessageHandler(Message arg1, CancellationToken arg2)
        {
            Console.WriteLine(arg1);
            await queueClient.CompleteAsync(arg1.SystemProperties.LockToken);
        }

        static Task ExceptionReceivedHandler(ExceptionReceivedEventArgs exceptionReceivedEventArgs)
        {
            Console.WriteLine(exceptionReceivedEventArgs.Exception);
            return Task.CompletedTask;
        }
    }
}
