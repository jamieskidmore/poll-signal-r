namespace PollSignalR.Models
{
    public class Poll
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public ICollection<Option> Options { get; set; }
    }
}