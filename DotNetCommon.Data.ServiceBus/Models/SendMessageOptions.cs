namespace DotNetCommon.Data.ServiceBus.Models;

public class SendMessageOptions
{
    public string MessageId { get; set; }

    public string SessionId { get; set; }

    public string PartitionKey { get; set; }
}
