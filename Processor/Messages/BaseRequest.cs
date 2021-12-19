using System;

namespace Processor.Messages
{
    public class BaseRequest
    {
        public BaseRequest()
        {
            RequestId = Guid.NewGuid().ToString();
            Timestamp = (DateTime.Now - DateTime.UnixEpoch).TotalSeconds;
        }
        
        public string RequestId { get; set; }
        public double Timestamp { get; set; }
    }
}