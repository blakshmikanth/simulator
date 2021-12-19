namespace Processor.Messages
{
    public class RequestAgentReady : BaseRequest
    {
        /// <summary>
        /// Agent Id
        /// </summary>
        public string Id { get; set; }
    }
}