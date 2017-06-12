namespace ConsoleSocialNetwork_Draft.Models {
	public class User {
		public string Name { get; }
		public Account Account { get; set; }

		public User(string name) {
			this.Name = name;
			this.Account = new Account();
		}
	}
}