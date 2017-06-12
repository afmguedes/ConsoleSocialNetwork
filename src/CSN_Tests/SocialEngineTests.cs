using System.Linq;
using System.Threading;
using ConsoleSocialNetwork_Draft;
using ConsoleSocialNetwork_Draft.Models;
using Xunit;

namespace CSN_Tests
{
    public class SocialEngineTests
    {
		[Fact]
		public void creates_user_when_first_posting() {
			var socialNetwork = new SocialNetwork();
			var userName = "Andre";
			var message = "Ola Mundo";

			socialNetwork.Compute($"{userName} -> {message}");

			Assert.NotEmpty(socialNetwork._socialEngine._registeredUsers);
			Assert.Equal(socialNetwork._socialEngine._registeredUsers.FirstOrDefault().Name, userName);
		}

		[Fact]
		public void post_new_message_to_existing_user_timeline() {
			var socialNetwork = new SocialNetwork();
			var userName = "Andre";
			var message = "Ola Mundo";
			var user = new User(userName);
			user.Account.AddPost(userName, message);
			socialNetwork._socialEngine._registeredUsers.Add(user);
			var newMessage = "Hello World";

			socialNetwork.Compute($"{userName} -> {newMessage}");

			Assert.NotEmpty(socialNetwork._socialEngine._registeredUsers);
			Assert.NotNull(socialNetwork._socialEngine._registeredUsers.FirstOrDefault(u => u.Name.Equals(userName)));
			Assert.NotNull(socialNetwork._socialEngine._registeredUsers.First(u => u.Name.Equals(userName)).Account.Timeline.FirstOrDefault(p => p.Item3.Equals(newMessage)));
		}

		[Fact]
		public void read_user_timeline() {
			var socialNetwork = new SocialNetwork();
			var userName = "Andre";
			var message01 = "Ola Mundo";
			var message02 = "Hello World";
			var message03 = "Ciao Mondo";
			var user = new User(userName);
			user.Account.AddPost(userName, message01);
			Thread.Sleep(1000); // Possibly not the best way to do this, appreciate some feedback.
			user.Account.AddPost(userName, message02);
			Thread.Sleep(1000); // Possibly not the best way to do this, appreciate some feedback.
			user.Account.AddPost(userName, message03);
			socialNetwork._socialEngine._registeredUsers.Add(user);

			var result = socialNetwork._socialEngine.Reading(userName);

			Assert.NotEmpty(result);
			Assert.Equal(result.Count, 3);
			Assert.Equal(result.Pop().Item3, message03);
			Assert.Equal(result.Pop().Item3, message02);
			Assert.Equal(result.Pop().Item3, message01);
		}

		[Fact]
		public void follow_user() {
			var socialNetwork = new SocialNetwork();
			var userName01 = "Andre";
			var userName02 = "Graziano";
			var message01 = "Ola Mundo";
			var message02 = "Ciao Mondo";
			var user01 = new User(userName01);
			var user02 = new User(userName02);
			user01.Account.AddPost(userName01, message01);
			user02.Account.AddPost(userName02, message02);
			socialNetwork._socialEngine._registeredUsers.Add(user01);
			socialNetwork._socialEngine._registeredUsers.Add(user02);

			socialNetwork.Compute($"{userName01} follows {userName02}");

			Assert.NotEmpty(socialNetwork._socialEngine._registeredUsers.FirstOrDefault(u => u.Name.Equals(userName01)).Account.FollowedUsers);
			Assert.NotNull(socialNetwork._socialEngine._registeredUsers.FirstOrDefault(u => u.Name.Equals(userName01)).Account.FollowedUsers.FirstOrDefault(u => u.Name.Equals(userName02)));
		}

		[Fact]
		public void read_wall() {
			var socialNetwork = new SocialNetwork();
			var userName01 = "Andre";
			var userName02 = "Graziano";
			var message01 = "Ola Mundo";
			var message02 = "Ciao Mondo";
			var user01 = new User(userName01);
			var user02 = new User(userName02);
			user01.Account.AddPost(userName01, message01);
			Thread.Sleep(1000); // Possibly not the best way to do this, appreciate some feedback.
			user02.Account.AddPost(userName02, message02);
			user01.Account.FollowUser(user02);
			socialNetwork._socialEngine._registeredUsers.Add(user01);
			socialNetwork._socialEngine._registeredUsers.Add(user02);

			var result = socialNetwork._socialEngine.Wall(userName01);

			Assert.NotEmpty(result);
			Assert.Equal(result.Count, 2);
			Assert.Equal(result[0].Item1, userName02);
			Assert.Equal(result[0].Item3, message02);
			Assert.Equal(result[1].Item1, userName01);
			Assert.Equal(result[1].Item3, message01);
		}
	}
}
