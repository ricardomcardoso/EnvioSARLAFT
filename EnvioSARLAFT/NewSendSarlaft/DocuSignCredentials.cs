using System;
using System.Configuration;
using DocuSign.eSign.Api;
using DocuSign.eSign.Client;
using Configuration = DocuSign.eSign.Client.Configuration;

namespace EnvioSARLAFT.NewSendSarlaft
{
  public class DocuSignCredentials
  {
    public string Username { get; set; }
    public string Password { get; set; }
    public string IntegratorKey { get; set; }
    public string AccountId { get; set; }

    public DocuSignCredentials()
    {
      ConfigureApiClient();

      this.Username = ConfigurationManager.AppSettings["DocuSign_UserName"];
      this.Password = ConfigurationManager.AppSettings["DocuSign_Password"];
      this.IntegratorKey = ConfigurationManager.AppSettings["DocuSign_IntegratorKey"];

      GetDocuSignAccountId();
    }

    private void ConfigureApiClient()
    {
      var basePath = ConfigurationManager.AppSettings["DocuSign_ApiClient"];
      if (string.IsNullOrWhiteSpace(basePath))
        throw new ArgumentException("No se pudo recuperar la url de conexión de docuSign");

      // instantiate a new api client
      ApiClient apiClient = new ApiClient(basePath);

      // set client in global config so we don't need to pass it to each API object.
      Configuration.Default.ApiClient = apiClient;
    }

    private void GetDocuSignAccountId()
    {
      if (string.IsNullOrWhiteSpace(this.Username) || string.IsNullOrWhiteSpace(this.Password) || string.IsNullOrWhiteSpace(this.IntegratorKey))
        throw new ArgumentException("No se pudo recuperar las credenciales de docuSign");

      // configure 'X-DocuSign-Authentication' header
      var authHeader = "{\"Username\":\"" + this.Username + "\", \"Password\":\"" + this.Password +
                       "\", \"IntegratorKey\":\"" + this.IntegratorKey + "\"}";

      Configuration.Default.AddDefaultHeader("X-DocuSign-Authentication", authHeader);

      // login call is available in the authentication api 
      var authApi = new AuthenticationApi();
      var loginInfo = authApi.Login();

      // parse the first account ID that is returned (user might belong to multiple accounts)
      this.AccountId = loginInfo.LoginAccounts[0].AccountId;
    }
  }
}
