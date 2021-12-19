namespace Simulator.Engine.Directory
{
    public class AgentResource
    {
        public AgentResource()
        {
            
        }

        public AgentResource(string id, string firstName, string lastName, string extension)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Extension = extension;
        }
        
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Extension { get; set; }
    }
}