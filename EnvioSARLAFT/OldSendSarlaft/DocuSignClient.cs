﻿using DocuSign.eSign.Api;
using DocuSign.eSign.Client;

namespace EnvioSARLAFT.OldSendSarlaft
{
  public class DocuSignClient
	{
		public DocuSignClient(DocuSignCredentials credentials)
		{
			// initialize client for desired environment (for production change to www)
			var apiClient = new ApiClient("https://demo.docusign.net/restapi");

			// configure 'X-DocuSign-Authentication' header
			var authHeader = "{\"Username\":\"" + credentials.Username + "\", \"Password\":\"" + credentials.Password +
											 "\", \"IntegratorKey\":\"" + credentials.IntegratorKey + "\"}";

			Configuration.Default.AddDefaultHeader("X-DocuSign-Authentication", authHeader);

			// login call is available in the authentication api 
			var authApi = new AuthenticationApi();
			var loginInfo = authApi.Login();

			// parse the first account ID that is returned (user might belong to multiple accounts)
			this.AccountId = loginInfo.LoginAccounts[0].AccountId;
		}

		public string AccountId { get; set; }
	}
}
