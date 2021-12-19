namespace Processor.Messages
{
    public class RequestAgentNotReady : BaseRequest
    {
        /// <summary>
        /// Agent Id
        /// </summary>
        public string Id { get; set; }
    }
}