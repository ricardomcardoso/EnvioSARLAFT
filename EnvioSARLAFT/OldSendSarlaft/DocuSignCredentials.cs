﻿namespace EnvioSARLAFT.OldSendSarlaft
{
	public class DocuSignCredentials
	{
		public DocuSignCredentials(string username, string password, string integratorKey)
		{
			this.Username = username;
			this.Password = password;
			this.IntegratorKey = integratorKey;
		}

		public string Username { get; set; }

		public string Password { get; set; }

		public string IntegratorKey { get; set; }
	}
}
