namespace ConsoleSocialNetwork_Draft.Models {
	public abstract class Command {
		public CommandType _type { get; set; }

		public abstract void Execute();
	}

	public enum CommandType {
		Posting,
		Reading,
		Following,
		Wall
	}
}