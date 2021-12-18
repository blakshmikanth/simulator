using System;

namespace Simulator.Engine.Contracts
{
    public class BaseRequest
    {
        public BaseRequest()
        {
            Timestamp = (DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;
        }
        
        public string RequestId { get; set; }
        public double Timestamp { get; set; }
    }
}