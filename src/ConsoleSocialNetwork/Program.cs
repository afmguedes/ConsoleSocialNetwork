using System;

namespace ConsoleSocialNetwork_Draft {
	public class Program {
		public static void Main(string[] args) {

			var socialNetwork = new SocialNetwork();
			var input = string.Empty;

			while (!(input = Console.ReadLine()).ToLower().Equals("logout")) {
				socialNetwork.Compute(input);
			}

			Console.WriteLine("\n\nGood bye!");
			Console.ReadKey();
		}
	}
}