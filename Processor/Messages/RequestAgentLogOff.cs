namespace Processor.Messages
{
    public class RequestAgentLogOff : BaseRequest
    {
        /// <summary>
        /// Agent Id
        /// </summary>
        public string Id { get; set; }
    }
}