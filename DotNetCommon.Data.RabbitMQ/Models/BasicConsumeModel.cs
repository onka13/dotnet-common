using System.Collections.Generic;

namespace DotNetCommon.Data.RabbitMQ.Models;

/// <summary>
/// Basic Consume Model
/// </summary>
public class BasicConsumeModel
{
    public BasicConsumeModel()
    {
        Arguments = new Dictionary<string, object>();
        ConsumerTag = string.Empty;
    }

    public string Queue { get; set; }

    public bool AutoAck { get; set; }

    public string ConsumerTag { get; set; }

    public bool NoLocal { get; set; }

    public bool Exclusive { get; set; }

    public IDictionary<string, object> Arguments { get; set; }
}
