using System;
using ConsoleSocialNetwork_Draft.Models;
using ConsoleSocialNetwork_Draft.Services;

namespace ConsoleSocialNetwork_Draft {
	public class SocialNetwork {
		public SocialEngine _socialEngine = new SocialEngine();

		public void Compute(string input) {
			var command = Translator.Translate(_socialEngine, input);

			if (command == null) {
				Console.WriteLine("Wrong command format!");
				return;
			}

			command.Execute();
		}
	}
}