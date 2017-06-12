using System;
using System.Collections.Generic;

namespace ConsoleSocialNetwork_Draft.Models {
	public class Account {
		public Stack<Tuple<string, DateTime, string>> Timeline { get; }
		public List<User> FollowedUsers { get; }

		public Account() {
			this.Timeline = new Stack<Tuple<string, DateTime, string>>();
			this.FollowedUsers = new List<User>();
		}

		public void AddPost(string userName, string message) {
			this.Timeline.Push(new Tuple<string, DateTime, string>(userName, DateTime.Now, message));
		}

		public void FollowUser(User followedUser) {
			this.FollowedUsers.Add(followedUser);
		}
	}
}