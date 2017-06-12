using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleSocialNetwork_Draft.Models {
	public class SocialEngine {
		public HashSet<User> _registeredUsers = new HashSet<User>();

		public void Action(CommandType type, string userName, string message, string followedUser) {
			switch (type) {
				case CommandType.Posting:
					Posting(userName, message);
					break;
				case CommandType.Reading:
					var timeline = Reading(userName);
					if (timeline != null && timeline.Count > 0)
						foreach (var post in timeline)
							Console.WriteLine($"{post.Item3} ({GetPostElapsedTime(post.Item2)})");
					break;
				case CommandType.Following:
					Following(userName, followedUser);
					break;
				case CommandType.Wall:
					var wall = Wall(userName);
					if (wall != null && wall.Count > 0)
						foreach (var post in wall)
							Console.WriteLine($"{post.Item1} - {post.Item3} ({GetPostElapsedTime(post.Item2)})");
					break;
			}
		}

		public void Posting(string userName, string message) {
			var user = _registeredUsers.FirstOrDefault(u => u.Name.Equals(userName));

			if (_registeredUsers.Count == 0 || user == null)
				CreateNewUserBasedOnNewPosting(userName, message);
			else
				user.Account.AddPost(userName, message);
		}

		public Stack<Tuple<string, DateTime, string>> Reading(string userName) {
			var user = _registeredUsers.FirstOrDefault(u => u.Name.Equals(userName));

			if (user != null)
				return user.Account.Timeline;

			Console.WriteLine($"User \'{userName}\' doesn't exist. Try posting something first!");
			return null;
		}

		public void Following(string userName, string followedUser) {
			var user = _registeredUsers.FirstOrDefault(u => u.Name.Equals(userName));
			var userToFollow = _registeredUsers.FirstOrDefault(u => u.Name.Equals(followedUser));

			if (user == null) {
				Console.WriteLine($"User \'{userName}\' doesn't exist. Try posting something first!");
				return;
			}

			if (userToFollow == null) {
				Console.WriteLine($"User \'{followedUser}\' doesn't exist. Try posting something first!");
				return;
			}

			user.Account.FollowUser(userToFollow);
		}

		public List<Tuple<string, DateTime, string>> Wall(string userName) {
			var user = _registeredUsers.FirstOrDefault(u => u.Name.Equals(userName));

			if (user == null) {
				Console.WriteLine($"User \'{userName}\' doesn't exist. Try posting something first!");
				return null;
			}

			var userWall = user.Account.Timeline.ToList();

			foreach (var followedUser in user.Account.FollowedUsers)
				userWall.AddRange(followedUser.Account.Timeline.ToList());

			userWall.Sort((t1, t2) => t2.Item2.CompareTo(t1.Item2));

			return userWall;
		}


		private void CreateNewUserBasedOnNewPosting(string userName, string message) {
			var newAccount = new Account();
			newAccount.AddPost(userName, message);

			var newUser = new User(userName) { Account = newAccount };

			_registeredUsers.Add(newUser);
		}

		private static string GetPostElapsedTime(DateTime postDate) {
			var elapsed = Math.Floor(DateTime.Now.Subtract(postDate).TotalSeconds);

			if (elapsed <= 59)
				return $"{elapsed} seconds ago";

			elapsed = Math.Floor(DateTime.Now.Subtract(postDate).TotalMinutes);
			return $"{elapsed} minutes ago";
		}
	}
}