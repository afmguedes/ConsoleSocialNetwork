using System;
using System.Linq;
using ConsoleSocialNetwork_Draft.Models;

namespace ConsoleSocialNetwork_Draft.Services {
	public static class Translator {
		public static UserCommand Translate(SocialEngine engine, string consoleCommand) {
			try {
				var parameters = consoleCommand.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);

				if (parameters != null && parameters.Length > 0) {
					if (parameters.Length == 1)
						return new UserCommand(engine, CommandType.Reading, parameters[0], string.Empty, string.Empty);
					if (parameters.Length == 2 && parameters[1].ToLower().Equals("wall"))
						return new UserCommand(engine, CommandType.Wall, parameters[0], string.Empty, string.Empty);
					if (parameters.Length >= 3 && parameters[1].ToLower().Equals("follows"))
						return new UserCommand(engine, CommandType.Following, parameters[0], string.Empty, parameters[2]);
					if (parameters.Length >= 3 && parameters[1].Equals("->")) {
						return new UserCommand(engine, CommandType.Posting, parameters[0], consoleCommand.Substring(parameters[0].Length + 4), string.Empty);
					}
				}
			} catch (Exception e) {
				Console.WriteLine($"Failed to read command: {e.Message}");
			}

			return null;
		}
	}
}