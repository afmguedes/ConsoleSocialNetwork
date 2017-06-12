namespace ConsoleSocialNetwork_Draft.Models {
	public class UserCommand : Command {
		private string _userName;
		private string _message;
		private string _followeduser;
		private SocialEngine _engine;

		public UserCommand(SocialEngine engine, CommandType type, string userName, string message, string followedUser) {
			this._engine = engine;
			this._type = type;
			this._userName = userName;
			this._message = message;
			this._followeduser = followedUser;
		}

		public override void Execute() {
			_engine.Action(_type, _userName, _message, _followeduser);
		}
	}
}