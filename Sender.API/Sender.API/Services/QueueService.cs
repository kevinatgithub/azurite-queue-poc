using Azure.Storage.Queues.Models;
using Azure.Storage.Queues;

namespace Sender.API.Services;

public class QueueService
{
    private readonly QueueClient _queueClient;

    public QueueService()
    {
        _queueClient = new QueueClient("UseDevelopmentStorage=true;", "app1-queue");
    }

    public async Task SendMessageAsync(string message)
    {
        await _queueClient.CreateIfNotExistsAsync();
        var encoded = Base64Encode(message);
        await _queueClient.SendMessageAsync(encoded);
    }

    public async Task<string> ReceiveMessageAsync()
    {
        QueueMessage[] messages = await _queueClient.ReceiveMessagesAsync(maxMessages: 1);

        if (messages.Length == 0)
        {
            return null;
        }

        QueueMessage message = messages[0];

        await _queueClient.DeleteMessageAsync(message.MessageId, message.PopReceipt);

        return message.MessageText;
    }

    private string Base64Encode(string plainText)
    {
        var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
        return System.Convert.ToBase64String(plainTextBytes);
    }
}
