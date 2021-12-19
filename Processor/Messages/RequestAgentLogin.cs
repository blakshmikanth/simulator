namespace Processor.Messages
{
    public class RequestAgentLogin: BaseRequest
    {
        /// <summary>
        /// Agent Id
        /// </summary>
        public string Id { get; set; }
    }
}