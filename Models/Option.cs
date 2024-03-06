namespace PollSignalR.Models 
{ 
	public class Option
	{
		public int Id { get; set; }
		public string Text { get; set; }
		public int VoteCount { get; set; }
		public int PollId { get; set; }
	}
}